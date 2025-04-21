using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public bool NeedeedPressKeyboard;
    public string NextQuestName;

    private bool playerInside = false;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        if (NeedeedPressKeyboard && playerInside && Input.GetKeyDown(KeyCode.F))
        {
            UpdateText();
            Destroy(gameObject);
        }
    }

    private void UpdateText()
    {
        questText.text = "Текущее задание:" + '\n' + NextQuestName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (NeedeedPressKeyboard)
            {
                playerInside = true;
            }
            else
            {
                UpdateText();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
