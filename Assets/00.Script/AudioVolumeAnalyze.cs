using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolumeAnalyzer : MonoBehaviour
{
    public float volume; // 현재 볼륨 (0~1)

    private AudioSource audioSource;
    private string micDevice;
    private float[] samples = new float[256];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0];
            int sampleRate = AudioSettings.outputSampleRate;

            audioSource.clip = Microphone.Start(micDevice, true, 1, sampleRate);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(micDevice) > 0)) { } // 시작 대기
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("마이크 없음");
        }
    }

    void Update()
    {
        audioSource.GetOutputData(samples, 0); // 시간 도메인 데이터
        float sum = 0f;

        foreach (var sample in samples)
        {
            sum += sample * sample;
        }

        volume = Mathf.Sqrt(sum / samples.Length); // RMS
    }
}