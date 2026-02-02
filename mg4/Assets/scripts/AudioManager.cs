using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip flapClip;
    public AudioClip scoreClip;
    public AudioClip gameOverClip;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnFlapSound()
    {
        if (audioSource != null && flapClip != null)
            audioSource.PlayOneShot(flapClip);
    }

    public void OnScoreIncreased()
    {
        if (audioSource != null && scoreClip != null)
            audioSource.PlayOneShot(scoreClip);
    }

    public void OnGameOverPlayHit()
    {
        if (audioSource != null && gameOverClip != null)
            audioSource.PlayOneShot(gameOverClip);
    }
}