using UnityEngine;

public class movee : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 moveVector;
    public QuestManager questUI;

    // --- Поля для спрайтов анимации ---
    [Header("Animation Sprites")]
    public Sprite[] frontSprites; // Вниз
    public Sprite[] backSprites;  // Вверх
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;
    public Sprite[] upLeftSprites;
    public Sprite[] upRightSprites;
    public Sprite[] downLeftSprites;
    public Sprite[] downRightSprites;

    // --- Настройки анимации ---
    [Header("Animation Settings")]
    public float timestandart = 0.5f; // Время между сменой кадров анимации

    // --- Спрайт для состояния покоя ---
    [Header("Idle Settings")]
    public Sprite standartPoz; // <<< Новый спрайт для стандартного положения (покоя)

    private SpriteRenderer spriteRenderer;
    // private Sprite initialSprite; // <<< Больше не нужно
    private Sprite[] currentAnimationSprites; // Текущий массив спрайтов для анимации
    private int currentFrameIndex;
    private float animationTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Проверяем, есть ли SpriteRenderer
        if (spriteRenderer == null)
        {
            Debug.LogError("Компонент SpriteRenderer не найден на этом объекте!", this);
        }
        // else
        // {
              // initialSprite = spriteRenderer.sprite; // <<< Убираем сохранение начального спрайта
        // }

        // Устанавливаем начальный спрайт покоя, если он назначен
        if (standartPoz != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = standartPoz;
        }
        else if (spriteRenderer != null && spriteRenderer.sprite == null)
        {
             Debug.LogWarning("StandartPoz sprite не назначен, и у SpriteRenderer нет начального спрайта.", this);
             // Можно установить какой-то дефолтный спрайт, например, первый кадр frontSprites
             // if(frontSprites != null && frontSprites.Length > 0) spriteRenderer.sprite = frontSprites[0];
        }


        // Инициализируем переменные анимации
        currentFrameIndex = 0;
        animationTimer = 0f;
        currentAnimationSprites = null; // Начнем без активной анимации
    }

    void Update()
    {
        // --- Обработка ввода ---
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        moveVector.Normalize(); // Нормализуем вектор

        // --- Обновление анимации ---
        UpdateAnimation();

        // --- Обработка UI квестов ---
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questUI.ToggleScroll();
        }
    }

    void FixedUpdate()
    {
        // --- Перемещение персонажа ---
        rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        bool isMoving = moveVector != Vector2.zero;

        if (isMoving)
        {
            // --- Логика анимации движения (остается прежней) ---
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
             // Если массив анимации для текущего направления не назначен или пуст,
             // покажем спрайт покоя как запасной вариант
            else if (standartPoz != null && spriteRenderer != null)
            {
                 spriteRenderer.sprite = standartPoz;
            }
        }
        else // --- Если персонаж НЕ двигается (стоим на месте) ---
        {
            // Устанавливаем спрайт стандартного положения, если он назначен
            if (spriteRenderer != null && standartPoz != null)
            {
                // Ставим только если текущий спрайт - не standartPoz, чтобы избежать лишних вызовов
                if (spriteRenderer.sprite != standartPoz)
                {
                    spriteRenderer.sprite = standartPoz;
                }
            }
            // Если standartPoz не назначен, персонаж останется с последним кадром анимации движения

            // Сбрасываем состояние анимации движения, чтобы она началась сначала при следующем движении
            currentFrameIndex = 0;
            animationTimer = 0f;
            currentAnimationSprites = null; // Указываем, что активной анимации движения нет
        }
    }
}