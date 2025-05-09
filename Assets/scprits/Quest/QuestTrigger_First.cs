using UnityEngine;

public class QuestTrigger_First : MonoBehaviour
{
    public QuestPanelController questUI;
    private bool playerInRange = false;

    void Start()
    {
        if (questUI != null && questUI.gameObject.activeSelf)
        {
            questUI.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !questUI.gameObject.activeSelf)
        {
            questUI.ShowQuest();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}