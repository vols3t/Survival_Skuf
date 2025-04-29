using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(AudioSource))]
public class movee : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveVector;

    [Header("Movement")]
    public float speed = 5f;

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

    [Header("Footstep Sound")]
    public AudioSource footstepSource;  // перетащите сюда AudioSource
    public AudioClip footstepClip;      // перетащите сюда AudioClip шага
    public float stepInterval = 0.4f;   // время между шагами

    public QuestManager questUI;

    private Sprite[] currentAnimationSprites;
    private int currentFrameIndex;
    private float animationTimer;

    private float stepTimer;

    void Start()
    {
        // кэш
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // инициализируем спрайт простоя
        if (standartPoz != null)
            spriteRenderer.sprite = standartPoz;

        // настраиваем AudioSource
        footstepSource = footstepSource ?? GetComponent<AudioSource>();
        footstepSource.playOnAwake = false;
        footstepSource.loop = false;
        stepTimer = stepInterval;

        // сброс анимации
        currentFrameIndex = 0;
        animationTimer = 0f;
        currentAnimationSprites = null;
    }

    void Update()
    {
        // ввод
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        moveVector.Normalize();

        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Q))
            questUI.ToggleScroll();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        bool isMoving = moveVector != Vector2.zero;

        // ----- FOOTSTEP SOUND -----
        if (isMoving && footstepSource != null && footstepClip != null)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                footstepSource.PlayOneShot(footstepClip);
                stepTimer = 0f;
            }
        }
        else
        {
            // сброс при остановке, чтобы при следующем старте сразу был шаг
            stepTimer = stepInterval;
        }

        // ----- SPRITE ANIMATION -----
        if (isMoving)
        {
            Sprite[] targetSprites = null;
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;

            if (angle > -22.5f && angle <= 22.5f)         targetSprites = rightSprites;
            else if (angle > 22.5f && angle <= 67.5f)    targetSprites = upRightSprites;
            else if (angle > 67.5f && angle <= 112.5f)   targetSprites = backSprites;
            else if (angle > 112.5f && angle <= 157.5f)  targetSprites = upLeftSprites;
            else if (angle > 157.5f || angle <= -157.5f) targetSprites = leftSprites;
            else if (angle > -157.5f && angle <= -112.5f)targetSprites = downLeftSprites;
            else if (angle > -112.5f && angle <= -67.5f) targetSprites = frontSprites;
            else if (angle > -67.5f && angle <= -22.5f)  targetSprites = downRightSprites;

            if (targetSprites != currentAnimationSprites)
            {
                currentAnimationSprites = targetSprites;
                currentFrameIndex = 0;
                animationTimer = 0f;
            }

            animationTimer += Time.deltaTime;
            if (animationTimer >= timestandart)
            {
                animationTimer -= timestandart;
                if (currentAnimationSprites != null && currentAnimationSprites.Length > 0)
                    currentFrameIndex = (currentFrameIndex + 1) % currentAnimationSprites.Length;
                else
                    currentFrameIndex = 0;
            }

            if (currentAnimationSprites != null && currentAnimationSprites.Length > 0)
                spriteRenderer.sprite = currentAnimationSprites[currentFrameIndex];
            else if (standartPoz != null)
                spriteRenderer.sprite = standartPoz;
        }
        else
        {
            // стоим на месте
            if (spriteRenderer.sprite != standartPoz && standartPoz != null)
                spriteRenderer.sprite = standartPoz;

            currentFrameIndex = 0;
            animationTimer = 0f;
            currentAnimationSprites = null;
        }
    }
}
