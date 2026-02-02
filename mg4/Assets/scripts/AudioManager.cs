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
        GameEvents.OnScoreChanged += OnScoreIncreased;
        GameEvents.OnGameOver += OnGameOverPlayHit;
        GameEvents.OnPlayerFlap += OnFlapSound;
    }

    public void OnFlapSound()
    {
        if (audioSource != null && flapClip != null)
            audioSource.PlayOneShot(flapClip);
    }

    public void OnScoreIncreased(int newScore)
    {
        if (audioSource != null && scoreClip != null)
            audioSource.PlayOneShot(scoreClip);
    }

    public void OnGameOverPlayHit()
    {
        if (audioSource != null && gameOverClip != null)
            audioSource.PlayOneShot(gameOverClip);
    }

    void OnDestroy()
    {
        GameEvents.OnScoreChanged -= OnScoreIncreased;
        GameEvents.OnGameOver -= OnGameOverPlayHit;
        GameEvents.OnPlayerFlap -= OnFlapSound;
    }
}