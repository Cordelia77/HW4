using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float minY = -4f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameOver)
        {
            rb.velocity = new Vector2(0, jumpForce);
            GameEvents.TriggerPlayerFlap();
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameManager.Instance.IsGameOver) return;

        if (other.gameObject.CompareTag("Pipe"))
        {
            GameManager.Instance.GameOver();
        }
    }
}