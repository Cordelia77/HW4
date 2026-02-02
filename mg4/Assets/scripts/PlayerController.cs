using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5.5f;
    private Rigidbody2D rb;
    private float minY = -3.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameOver)
        {
            rb.velocity = new Vector2(0, jumpForce);
            if (AudioManager.Instance != null) AudioManager.Instance.OnFlapSound();
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            if (GameManager.Instance != null && !GameManager.Instance.IsGameOver)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}