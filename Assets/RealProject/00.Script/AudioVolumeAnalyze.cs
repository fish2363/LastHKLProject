using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolumeAnalyzer : MonoBehaviour
{
    public float volume; // ���� ���� (0~1)
    private AudioSource audioSource;
    private float[] samples = new float[256];

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource.clip != null && audioSource.isPlaying)
        {
            audioSource.GetOutputData(samples, 0); // �ð� ������
            float sum = 0f;
            foreach (var s in samples)
                sum += s * s;
            volume = Mathf.Sqrt(sum / samples.Length); // RMS
        }
    }
}