using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;

    
    private string[] quests = {
        "Пройти на кухню",
        "Открыть холодос"
    };

    private int currentQuestIndex = 0;

    void Start()
    {
        UpdateQuestText();
    }

    public void NextQuest()
    {
        currentQuestIndex++;
        if (currentQuestIndex < quests.Length)
        {
            UpdateQuestText();
        }
        else
        {
            questText.text = "Все квесты выполнены!";
        }
    }

    private void UpdateQuestText()
    {
        questText.text = "Текущее задание:" + '\n' + quests[currentQuestIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NextQuest();
            Destroy(gameObject);
        }
    }
}
