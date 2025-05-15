using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Основные настройки")]
    public Transform teleportTarget;  // Точка телепортации
    public float CameraPosX, CameraPosY;  // Координаты камеры после телепорта
    public KeyCode teleportKey = KeyCode.F;  // Клавиша активации

    [Header("Визуальные настройки")]
    public Sprite newSprite;  // Спрайт при приближении
    public bool changeSprite = true;  // Нужно ли менять спрайт
    
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerInRange = false;
    private GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
            
            // Меняем спрайт при приближении
            if (changeSprite && newSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            
            // Возвращаем исходный спрайт
            if (changeSprite && spriteRenderer != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }

    private void TeleportPlayer()
    {
        if (player != null && teleportTarget != null)
        {
            // Телепортируем игрока
            player.transform.position = teleportTarget.position;
            
            // Перемещаем камеру
            if (Camera.main != null)
            {
                Camera.main.transform.position = new Vector3(
                    CameraPosX,
                    CameraPosY,
                    Camera.main.transform.position.z
                );
            }
        }
    }
}