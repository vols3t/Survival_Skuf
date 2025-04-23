using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public ScrollAnimator scrollAnimator;
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
        UpdateQuestText();
    }

    public void ToggleScroll()
    {
        if (!isVisible)
        {
            scrollAnimator.PlayOpen(() =>
            {
                questTextCanvas.alpha = 1;
                isVisible = true;
            });
        }
        else
        {
            questTextCanvas.alpha = 0;
            scrollAnimator.PlayClose(() =>
            {
                isVisible = false;
            });
        }
    }

    public void TriggerNextQuest()
    {
        if (currentQuestIndex >= quests.Count) return;

        // Добавляем текущий квест в выполненные
        if (currentQuestIndex >= 0)
        {
            completedQuests.Add(quests[currentQuestIndex]);
        }

        // Переходим к следующему квесту
        currentQuestIndex++;

        // Если набрали 4 квеста - очищаем и увеличиваем страницу
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

        int startIndex = currentPage * QUESTS_PER_PAGE;
        for (int i = startIndex; i < completedQuests.Count + startIndex && i < quests.Count; i++)
        {
            fullText += $"<s>{quests[i]}</s>\n\n";
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
}