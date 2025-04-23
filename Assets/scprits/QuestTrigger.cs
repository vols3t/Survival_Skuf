using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public bool IsNeededAction = false;
    private bool isPlayerInTrigger = false;

    private void Update()
    {
        if (IsNeededAction && isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            QuestManager.Instance.TriggerNextQuest();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsNeededAction && other.CompareTag("Player"))
        {
            QuestManager.Instance.TriggerNextQuest();
            Destroy(gameObject);
        }
    }
}