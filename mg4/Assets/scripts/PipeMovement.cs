using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float destroyX = -10f;

    void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.AddScore();
        }
    }
}