using System.Collections;
using UnityEngine;
using TMPro;

public class ThoughtBoxController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textDisplay;
    public float fadeDuration = 0.3f;
    public float charDelay = 0.03f;
    public float visibleTime;
    private Coroutine currentRoutine;

    private void Start()
    {
        canvasGroup.alpha = 0f; // Скрываем при старте
    }

    public void ShowThought(string thought)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(PlayThought(thought, visibleTime));
    }

    private IEnumerator PlayThought(string thought, float visibleTime)
    {
        yield return Fade(0, 1);

        textDisplay.text = "";
        foreach (char c in thought)
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(charDelay);
        }

        float timer = 0f;
        while (timer < visibleTime)
        {
            if (Input.GetMouseButtonDown(1)) 
                break;

            timer += Time.deltaTime;
            yield return null;
        }

        yield return Fade(1, 0);
        currentRoutine = null;
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
