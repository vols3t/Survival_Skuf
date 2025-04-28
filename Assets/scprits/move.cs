using UnityEngine;

public class movee : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 moveVector;
    public QuestManager questUI;

    [Header("Animation Sprites")]
    public Sprite[] frontSprites;
    public Sprite[] backSprites;
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;
    public Sprite[] upLeftSprites;
    public Sprite[] upRightSprites;
    public Sprite[] downLeftSprites;
    public Sprite[] downRightSprites;

    [Header("Animation Settings")]
    public float timestandart = 0.5f;

    [Header("Idle Settings")]
    public Sprite standartPoz;

    private SpriteRenderer spriteRenderer;
    private Sprite[] currentAnimationSprites;
    private int currentFrameIndex;
    private float animationTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("Компонент SpriteRenderer не найден на этом объекте!", this);
        }

        if (standartPoz != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = standartPoz;
        }
        else if (spriteRenderer != null && spriteRenderer.sprite == null)
        {
             Debug.LogWarning("StandartPoz sprite не назначен, и у SpriteRenderer нет начального спрайта.", this);
        }

        currentFrameIndex = 0;
        animationTimer = 0f;
        currentAnimationSprites = null;
    }

    void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        moveVector.Normalize();

        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            questUI.ToggleScroll();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        bool isMoving = moveVector != Vector2.zero;

        if (isMoving)
        {
            Sprite[] targetAnimationSprites = null;
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;

            if (angle > -22.5f && angle <= 22.5f)         targetAnimationSprites = rightSprites;
            else if (angle > 22.5f && angle <= 67.5f)    targetAnimationSprites = upRightSprites;
            else if (angle > 67.5f && angle <= 112.5f)   targetAnimationSprites = backSprites;
            else if (angle > 112.5f && angle <= 157.5f)  targetAnimationSprites = upLeftSprites;
            else if (angle > 157.5f || angle <= -157.5f) targetAnimationSprites = leftSprites;
            else if (angle > -157.5f && angle <= -112.5f)targetAnimationSprites = downLeftSprites;
            else if (angle > -112.5f && angle <= -67.5f) targetAnimationSprites = frontSprites;
            else if (angle > -67.5f && angle <= -22.5f)  targetAnimationSprites = downRightSprites;

            if (targetAnimationSprites != currentAnimationSprites || currentAnimationSprites == null)
            {
                currentAnimationSprites = targetAnimationSprites;
                currentFrameIndex = 0;
                animationTimer = 0f;
            }

            animationTimer += Time.deltaTime;

            if (animationTimer >= timestandart)
            {
                animationTimer -= timestandart;
                if (currentAnimationSprites != null && currentAnimationSprites.Length > 0)
                {
                    currentFrameIndex = (currentFrameIndex + 1) % currentAnimationSprites.Length;
                }
                else { currentFrameIndex = 0; }
            }

            if (currentAnimationSprites != null && currentAnimationSprites.Length > 0)
            {
                if (currentFrameIndex >= currentAnimationSprites.Length) { currentFrameIndex = 0; }
                spriteRenderer.sprite = currentAnimationSprites[currentFrameIndex];
            }
            else if (standartPoz != null && spriteRenderer != null)
            {
                 spriteRenderer.sprite = standartPoz;
            }
        }
        else
        {
            if (spriteRenderer != null && standartPoz != null)
            {
                if (spriteRenderer.sprite != standartPoz)
                {
                    spriteRenderer.sprite = standartPoz;
                }
            }

            currentFrameIndex = 0;
            animationTimer = 0f;
            currentAnimationSprites = null;
        }
    }
}