using Ami.BroAudio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ESCManager : MonoBehaviour
{
    public static ESCManager Instance;
    public bool IsGameStart { get; set; }

    private bool isOn;
    [SerializeField] CanvasGroup escCanvas;

    [SerializeField] private BroAudioType _bgm;
    [SerializeField] private BroAudioType _sfx;
    [SerializeField] private BroAudioType _main;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;


    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private List<Resolution> resolutions=new();
    private bool _isInitialized;


    public TMP_Dropdown micDropdown;
    public AudioSource targetAudioSource; // AudioVolumeAnalyzer에서 사용하는 AudioSource

    private string currentDevice;
    private List<string> deviceList = new();

    private const string PREF_KEY = "LastMicDevice";

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UNLockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Start()
    {
        SetupResolutionDropdown();
        SetupFullscreenToggle(fullscreenToggle.isOn);

        resolutionDropdown.onValueChanged.AddListener(OnResolutionSelected);
        fullscreenToggle.onValueChanged.AddListener(SetupFullscreenToggle);


        RefreshMicList();

        micDropdown.onValueChanged.AddListener(SelectMicrophone);

        // PlayerPrefs로부터 저장된 마이크 복원
        if (Microphone.devices.Length > 0)
        {
            string savedMic = PlayerPrefs.GetString(PREF_KEY, "");
            int index = deviceList.IndexOf(savedMic);
            if (index >= 0)
            {
                micDropdown.value = index;
                micDropdown.RefreshShownValue();
                SelectMicrophone(index);
            }
            else
            {
                SelectMicrophone(0); // fallback
            }
        }
    }

    public void RefreshMicList()
    {
        deviceList = new List<string>(Microphone.devices);
        micDropdown.ClearOptions();

        if (deviceList.Count == 0)
        {
            micDropdown.AddOptions(new List<string> { "마이크 없음" });
            micDropdown.interactable = false;
            return;
        }

        micDropdown.interactable = true;
        micDropdown.AddOptions(deviceList);
    }

    public void SelectMicrophone(int index)
    {
        if (deviceList.Count == 0) return;

        currentDevice = deviceList[index];
        PlayerPrefs.SetString(PREF_KEY, currentDevice);
        PlayerPrefs.Save();

        // 기존 마이크 끄기
        if (Microphone.IsRecording(currentDevice))
            Microphone.End(currentDevice);

        int sampleRate = AudioSettings.outputSampleRate;

        targetAudioSource.Stop();
        targetAudioSource.clip = Microphone.Start(currentDevice, true, 1, sampleRate);
        targetAudioSource.loop = true;
        StartCoroutine(WaitAndPlay());
    }

    IEnumerator WaitAndPlay()
    {
        while (Microphone.GetPosition(currentDevice) <= 0)
            yield return null;

        targetAudioSource.Play();
    }


    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            ESC();
    }

    void SetupResolutionDropdown()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution r = Screen.resolutions[i];
            float aspect = (float)r.width / r.height;
            float refreshRate = r.refreshRateRatio.numerator / (float)r.refreshRateRatio.denominator;

            if (Mathf.Abs(aspect - (16f / 9f)) < 0.1f && r.width >= 1280)
            {
                resolutions.Add(r);
            }
        }
        resolutionDropdown.ClearOptions();
        resolutions.Reverse();

        var options = resolutions.Select(r => $"{r.width} x {r.height}").ToList();
        resolutionDropdown.AddOptions(options);

        _isInitialized = true;
    }

    void SetupFullscreenToggle(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    void OnResolutionSelected(int index)
    {
        if (!_isInitialized) return;

        bool isFullscreen = fullscreenToggle.isOn;
        Resolution res = resolutions[index];

        Screen.SetResolution(res.width, res.height, isFullscreen);
    }

    

    public void ESC()
    {
        isOn = !isOn;
        if (!isOn) { Time.timeScale = 1f; }
        if (isOn) UNLockMouse();
        else if(IsGameStart) LockMouse();

        float force = isOn ? 1 : 0;
        escCanvas.blocksRaycasts = isOn;
        escCanvas.interactable = isOn;
        escCanvas.DOFade(force, 0.2f).OnComplete(()=> { if (isOn) { Time.timeScale = 0f; } });
    }


    public void BGM(float volume)
    {
        _bgmSlider.value = volume;
        BroAudio.SetVolume(_bgm, volume);
    }

    public void SFX(float volume)
    {
        _sfxSlider.value = volume;
        BroAudio.SetVolume(_sfx, volume);
    }

    public void Master(float volume)
    {
        _masterSlider.value = volume;
        BroAudio.SetVolume(_main, volume);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 실행 중이면 Play 모드를 끔
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서는 애플리케이션 종료
        Application.Quit();
#endif
    }
}
