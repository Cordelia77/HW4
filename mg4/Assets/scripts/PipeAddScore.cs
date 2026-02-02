using UnityEngine;

public class PipeAddScore : MonoBehaviour
{
    private bool hasScored = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasScored && GameManager.Instance != null)
        {
            GameManager.Instance.AddScore();
            hasScored = true;
            Destroy(this);
        }
    }
}