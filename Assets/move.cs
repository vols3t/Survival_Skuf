using UnityEngine;

public class movee : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    private Vector2 moveVector;

    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        if (moveVector != Vector2.zero)
        {
            Flip();
        }

        rb.MovePosition(rb.position + moveVector.normalized * speed * Time.deltaTime);
    }

    void Flip()
    {
        if (moveVector.x < 0)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (moveVector.x > 0)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (moveVector.y > 0)
        {
            spriteRenderer.sprite = backSprite;
        }
        else if (moveVector.y < 0)
        {
            spriteRenderer.sprite = frontSprite;
        }
    }
}
