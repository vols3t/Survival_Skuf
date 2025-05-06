using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeTrigger : MonoBehaviour
{
    public Image fadeImage;        
    public float fadeDuration = 1.5f;
    public float darkTime = 1.5f;
    public Canvas fadeCanvas;
    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FadeRoutine());
        }
    }

    private IEnumerator FadeRoutine()
    {
        fadeCanvas.sortingOrder = 100;
        yield return StartCoroutine(FadeTo(1f));
        yield return new WaitForSeconds(darkTime);

        yield return StartCoroutine(FadeTo(0f));
        fadeCanvas.sortingOrder = 0;
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / fadeDuration);
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, blend);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
