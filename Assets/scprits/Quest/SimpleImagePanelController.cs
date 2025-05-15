using UnityEngine;

public class SimpleImagePanelController : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject objectToShow;  // Объект с картинкой (должен иметь SpriteRenderer)
    public Sprite newTriggerSprite;   // Спрайт, который появится при приближении к триггеру
    public KeyCode interactionKey = KeyCode.F; // Клавиша активации

    [Header("Пауза игры")]
    public bool pauseGame = true;     // Останавливать ли время при показе картинки

    private SpriteRenderer triggerSpriteRenderer; // SpriteRenderer текущего объекта (триггера)
    private Sprite originalTriggerSprite;         // Исходный спрайт триггера
    private bool playerInRange = false;

    private void Start()
    {
        // Находим SpriteRenderer у текущего объекта (если есть)
        triggerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (triggerSpriteRenderer != null)
        {
            originalTriggerSprite = triggerSpriteRenderer.sprite;
        }

        // Выключаем объект с картинкой при старте
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            ToggleObject();
        }
    }

    // Включить/выключить объект с картинкой
    private void ToggleObject()
    {
        if (objectToShow == null) return;

        bool shouldShow = !objectToShow.activeSelf;
        objectToShow.SetActive(shouldShow);

        // Остановка времени (если включено)
        if (pauseGame)
        {
            Time.timeScale = shouldShow ? 0f : 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Меняем спрайт триггера (если задан newTriggerSprite)
            if (triggerSpriteRenderer != null && newTriggerSprite != null)
            {
                triggerSpriteRenderer.sprite = newTriggerSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Возвращаем исходный спрайт триггера
            if (triggerSpriteRenderer != null)
            {
                triggerSpriteRenderer.sprite = originalTriggerSprite;
            }

            // Автоматически скрываем картинку при выходе из зоны
            if (objectToShow != null)
            {
                objectToShow.SetActive(false);
                if (pauseGame) Time.timeScale = 1f;
            }
        }
    }
}