using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolumeAnalyzer : MonoBehaviour
{
    public float volume; // 현재 볼륨 (0~1)
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
            audioSource.GetOutputData(samples, 0); // 시간 도메인
            float sum = 0f;
            foreach (var s in samples)
                sum += s * s;
            volume = Mathf.Sqrt(sum / samples.Length); // RMS
        }
    }
}