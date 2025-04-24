using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public CanvasGroup questPanelCanvas;
    public CanvasGroup questTextCanvas;
    public TMP_Text questTextUI;
    public List<string> quests = new List<string>();

    private int currentQuestIndex = 0;
    private List<string> completedQuests = new List<string>();
    private bool isVisible = false;
    private const int QUESTS_PER_PAGE = 4;
    private int currentPage = 0;

    void Awake()
    {
        Instance = this;
        questPanelCanvas.alpha = 0;
        questTextCanvas.alpha = 0;
        questPanelCanvas.gameObject.SetActive(false);
        questTextCanvas.gameObject.SetActive(false);
        UpdateQuestText();
    }

    public void ToggleScroll()
    {
        if (!isVisible)
        {
            questPanelCanvas.gameObject.SetActive(true);
            questTextCanvas.gameObject.SetActive(true);
            StartCoroutine(FadeCanvasGroup(questPanelCanvas, 1f));
            StartCoroutine(FadeCanvasGroup(questTextCanvas, 1f));
            isVisible = true;
        }
        else
        {
            StartCoroutine(FadeCanvasGroup(questPanelCanvas, 0f, () =>
            {
                questPanelCanvas.gameObject.SetActive(false);
            }));
            StartCoroutine(FadeCanvasGroup(questTextCanvas, 0f, () =>
            {
                questTextCanvas.gameObject.SetActive(false);
            }));
            isVisible = false;
        }
    }

    public void TriggerNextQuest()
    {
        if (quests == null || quests.Count == 0) return;

        if (currentQuestIndex + 1 >= quests.Count) return;

        if (currentQuestIndex >= 0)
        {
            completedQuests.Add(quests[currentQuestIndex]);
        }

        currentQuestIndex++;

        if (completedQuests.Count >= QUESTS_PER_PAGE)
        {
            completedQuests.Clear();
            currentPage++;
        }

        UpdateQuestText();
    }



    private void UpdateQuestText()
    {
        string fullText = "";

        foreach (string completed in completedQuests)
        {
            fullText += $"<s>{completed}</s>\n\n";
        }

        if (currentQuestIndex < quests.Count)
        {
            fullText += quests[currentQuestIndex];
        }
        else
        {
            fullText += "Все квесты завершены!";
        }

        questTextUI.text = fullText;
    }



    private IEnumerator FadeCanvasGroup(CanvasGroup group, float targetAlpha, System.Action onComplete = null)
    {
        float startAlpha = group.alpha;
        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        group.alpha = targetAlpha;
        onComplete?.Invoke();
    }
}
