using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public float speed = 2f;
    public float directionChangeInterval = 2f;
    public Sprite spriteForward;
    public Sprite spriteBack;
    public Transform playerRespawnPoint;
    public Transform cameraRespawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChooseNewDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            ChooseNewDirection();
            timer = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void ChooseNewDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        if (moveDirection.y > 0)
            spriteRenderer.sprite = spriteBack;
        else
            spriteRenderer.sprite = spriteForward;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 normal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(moveDirection, normal);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerRespawnPoint.position;
            Camera.main.transform.position = new Vector3(
                cameraRespawnPoint.position.x,
                cameraRespawnPoint.position.y,
                Camera.main.transform.position.z);
        }
    }
}