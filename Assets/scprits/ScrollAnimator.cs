using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimator : MonoBehaviour
{
    public Image scrollImage;
    public Sprite[] animationFrames;
    public float frameDelay = 0.05f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void PlayOpen(Action onFinished)
    {
        StopAllCoroutines(); 
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        StartCoroutine(Play(animationFrames, onFinished));
    }

    public void PlayClose(Action onFinished)
    {
        StopAllCoroutines();
        StartCoroutine(PlayReversed(animationFrames, onFinished));
    }

    private IEnumerator Play(Sprite[] frames, Action onFinished)
    {
        foreach (var frame in frames)
        {
            scrollImage.sprite = frame;

            Color color = scrollImage.color;
            color.a = 1f;
            scrollImage.color = color;

            yield return new WaitForSeconds(frameDelay);
        }

        onFinished?.Invoke();
    }

    private IEnumerator PlayReversed(Sprite[] frames, Action onFinished)
    {
        for (int i = frames.Length - 1; i >= 0; i--)
        {
            scrollImage.sprite = frames[i];

            Color color = scrollImage.color;
            color.a = 1f;
            scrollImage.color = color;

            yield return new WaitForSeconds(frameDelay);
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        onFinished?.Invoke();
    }
}
