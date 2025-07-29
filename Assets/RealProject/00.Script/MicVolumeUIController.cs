using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MicVolumeUIController : MonoBehaviour
{
    public AudioVolumeAnalyzer analyzer;  // ����ũ ���� ���� Ŭ����
    public Image[] volumeBars;            // ���� �����
    public float maxVolume = 0.2f;        // �ִ� ���� ���� (�� �̻��̸� �� ��)
    public Color normalColor = Color.white;
    public Color dangerColor = Color.red;

    private bool isMaxed = false;
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    private bool isShaking;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // UI�� ��ġ ����
    }

    void Update()
    {
        float normalizedVolume = Mathf.Clamp01(analyzer.volume / maxVolume);
        int activeBars = Mathf.RoundToInt(normalizedVolume * volumeBars.Length);

        // ���� Ȱ��ȭ & �� ����
        for (int i = 0; i < volumeBars.Length; i++)
        {
            volumeBars[i].enabled = i < activeBars;

            if (volumeBars[i].enabled)
            {
                volumeBars[i].color = normalizedVolume >= 1f ? dangerColor : normalColor;
            }
        }

        // �ִ� ���� ���� �� ��鸲 & ������
        if (normalizedVolume >= 1f && !isMaxed && !isShaking)
        {
            isMaxed = true;

            isShaking = true;
            rectTransform.DOShakeRotation(0.3f, 10f, 10, 90f).OnComplete(() => {
                isShaking = false;
                //rectTransform.anchoredPosition = originalPosition; // ��ġ ����
            });
            
        }
        else if (normalizedVolume < 1f)
        {
            isMaxed = false;
        }
    }
}
