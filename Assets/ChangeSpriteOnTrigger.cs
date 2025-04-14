using UnityEngine;

public class ChangeSpriteOnTrigger : MonoBehaviour
{
    public Sprite newSprite; 
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = newSprite; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = originalSprite; 
        }
    }
}
