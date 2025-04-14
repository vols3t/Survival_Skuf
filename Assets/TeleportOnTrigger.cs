using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTarget;
    public Sprite newSprite;
    public float CameraPosX;
    public float CameraPosY;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;

    private bool playerIsInside = false;
    private GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (playerIsInside && Input.GetKeyDown(KeyCode.F))
        {
            if (player != null && teleportTarget != null)
            {
                player.transform.position = teleportTarget.position;

                Camera.main.transform.position = new Vector3(
                    CameraPosX,
                    CameraPosY,
                    Camera.main.transform.position.z
                );
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            player = other.gameObject;
            spriteRenderer.sprite = newSprite;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            player = null;
            spriteRenderer.sprite = originalSprite;
        }
    }
}
