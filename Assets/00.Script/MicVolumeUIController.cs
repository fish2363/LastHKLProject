using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MicVolumeUIController : MonoBehaviour
{
    public AudioVolumeAnalyzer analyzer;  // 마이크 볼륨 측정 클래스
    public Image[] volumeBars;            // 볼륨 막대들
    public float maxVolume = 0.2f;        // 최대 예상 볼륨 (이 이상이면 꽉 참)
    public Color normalColor = Color.white;
    public Color dangerColor = Color.red;

    private bool isMaxed = false;
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    private bool isShaking;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // UI용 위치 저장
    }

    void Update()
    {
        float normalizedVolume = Mathf.Clamp01(analyzer.volume / maxVolume);
        int activeBars = Mathf.RoundToInt(normalizedVolume * volumeBars.Length);

        // 막대 활성화 & 색 설정
        for (int i = 0; i < volumeBars.Length; i++)
        {
            volumeBars[i].enabled = i < activeBars;

            if (volumeBars[i].enabled)
            {
                volumeBars[i].color = normalizedVolume >= 1f ? dangerColor : normalColor;
            }
        }

        // 최대 볼륨 도달 시 흔들림 & 빨간색
        if (normalizedVolume >= 1f && !isMaxed && !isShaking)
        {
            isMaxed = true;

            isShaking = true;
            rectTransform.DOShakeRotation(0.3f, 10f, 10, 90f).OnComplete(() => {
                isShaking = false;
                //rectTransform.anchoredPosition = originalPosition; // 위치 복원
            });
            
        }
        else if (normalizedVolume < 1f)
        {
            isMaxed = false;
        }
    }
}
