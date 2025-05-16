using UnityEngine;
using System.Collections;

public class TeleportPairF : MonoBehaviour
{
    public enum ActivationType { Automatic, Manual }

    [Header("Основные настройки")]
    public Transform teleportTarget;
    public TeleportPairF pairedTeleporter;
    public ActivationType activationType = ActivationType.Automatic;
    public bool isActive = true;
    public float teleportDelay = 0f;

    [Header("Визуальные настройки")]
    public Sprite activeSprite;
    public bool changeSprite = true;

    [Header("Настройки камеры")]
    public float CameraPosX, CameraPosY;

    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerIsInside = false;
    private GameObject player;
    private bool isTeleporting = false;

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
        if (!isActive || !playerIsInside || isTeleporting)
            return;

        // Разная логика активации в зависимости от типа
        bool shouldTeleport = activationType == ActivationType.Automatic || 
                            (activationType == ActivationType.Manual && Input.GetKeyDown(KeyCode.F));

        if (shouldTeleport)
        {
            isTeleporting = true;
            if (teleportDelay > 0f)
                StartCoroutine(TeleportWithDelay());
            else
                Teleport();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            playerIsInside = true;
            player = other.gameObject;
            if (changeSprite && activeSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = activeSprite;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            player = null;
            if (changeSprite && spriteRenderer != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
            isTeleporting = false;
        }
    }

    private IEnumerator TeleportWithDelay()
    {
        yield return new WaitForSeconds(teleportDelay);
        Teleport();
    }

    private void Teleport()
    {
        if (player == null || teleportTarget == null || pairedTeleporter == null)
            return;

        pairedTeleporter.SetActive(false);
        player.transform.position = teleportTarget.position;
        
        if (Camera.main != null)
        {
            Camera.main.transform.position = new Vector3(
                CameraPosX,
                CameraPosY,
                Camera.main.transform.position.z
            );
        }

        StartCoroutine(EnablePairedTeleporter());
    }

    private IEnumerator EnablePairedTeleporter()
    {
        yield return new WaitForSeconds(0.5f);
        pairedTeleporter.SetActive(true);
    }

    public void SetActive(bool state)
    {
        isActive = state;
        if (!state && changeSprite && spriteRenderer != null)
        {
            spriteRenderer.sprite = originalSprite;
        }
    }
}