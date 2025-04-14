using UnityEngine;
using System.Collections;

public class ShortAnimationOnF : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite newSprite;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerIsInside = false;
    private bool isChanging = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (playerIsInside && !isChanging && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ChangeSpritesSequence());
        }
    }

    IEnumerator ChangeSpritesSequence()
    {
        isChanging = true;

        foreach (Sprite sprite in sprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(1f);
        }
        spriteRenderer.sprite = originalSprite;

        isChanging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;        
            spriteRenderer.sprite = newSprite;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            spriteRenderer.sprite = originalSprite;
        }
    }
}
