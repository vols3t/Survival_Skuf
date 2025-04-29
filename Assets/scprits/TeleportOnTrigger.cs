using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class TeleportOnTrigger : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource teleportSource; 
    public AudioClip   teleportClip;   

    [Header("Teleport")]
    public Transform teleportTarget;   
    public float     teleportDelay;    

    [Header("Sprite (optional)")]
    public Sprite newSprite;           
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool   playerIsInside;
    private bool   isTeleporting;
    private GameObject player;

    void Start()
    {
        spriteRenderer  = GetComponent<SpriteRenderer>();
        originalSprite  = spriteRenderer.sprite;

        // если в инспекторе AudioSource не проставлен — попробуем взять свой
        if (teleportSource == null)
            teleportSource = GetComponent<AudioSource>();

        if (teleportSource != null)
        {
            teleportSource.playOnAwake = false;
            teleportSource.loop        = false;
        }
    }

    void Update()
    {
        // Если игрок в триггере и не в процессе телепортации — ждём F
        if (playerIsInside && !isTeleporting && Input.GetKeyDown(KeyCode.F))
        {
            if (teleportDelay > 0f)
                StartCoroutine(DoTeleportWithDelay());
            else
                DoTeleport();
        }
    }

    private IEnumerator DoTeleportWithDelay()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(teleportDelay);
        DoTeleport();
    }

    private void DoTeleport()
    {
        // Проигрываем звук (если указано)
        if (teleportSource != null && teleportClip != null)
            teleportSource.PlayOneShot(teleportClip);

        // Перемещаем игрока и камеру
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
            var camPos = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(teleportTarget.position.x,
                                                         teleportTarget.position.y,
                                                         camPos.z);
        }

        isTeleporting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            player         = other.gameObject;
            if (newSprite != null)
                spriteRenderer.sprite = newSprite;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            player         = null;
            spriteRenderer.sprite = originalSprite;
            isTeleporting  = false;
        }
    }
}
