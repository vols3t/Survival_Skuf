using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(AudioSource), typeof(Collider2D))]
public class ShortAnimationOnF : MonoBehaviour
{
    [Header("Animation Sprites")]
    public Sprite[] sprites;        
    public Sprite   newSprite;     
    public float    spriteTime = .1f;

    [Header("Sound")]
    public AudioClip clip;         
    public AudioSource source;    
    private SpriteRenderer rend;
    private Sprite           originalSprite;
    private bool             playerIsInside;
    private bool             isChanging;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalSprite = rend.sprite;

        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
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

        if (clip != null)
            source.PlayOneShot(clip);

        foreach (var spr in sprites)
        {
            rend.sprite = spr;
            yield return new WaitForSeconds(spriteTime);
        }

        rend.sprite = originalSprite;
        isChanging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            if (!isChanging && newSprite != null)
                rend.sprite = newSprite;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            if (!isChanging)
                rend.sprite = originalSprite;
        }
    }
}
