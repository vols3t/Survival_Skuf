using UnityEngine;
using DialogueEditor;

public class SequentialDialogTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;
    [SerializeField] private GameObject nextTrigger; // Следующий триггер (если есть)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ConversationManager.Instance != null)
        {
            // Запускаем диалог и отключаем текущий триггер
            ConversationManager.Instance.StartConversation(conversation);
            GetComponent<Collider2D>().enabled = false;

            // Подписываемся на событие завершения диалога
            ConversationManager.OnConversationEnded += EnableNextTrigger;
        }
    }

    private void EnableNextTrigger()
    {
        // Включаем следующий триггер (если он есть)
        if (nextTrigger != null)
        {
            nextTrigger.GetComponent<Collider2D>().enabled = true;
        }

        // Отписываемся от события, чтобы не вызывалось повторно
        ConversationManager.OnConversationEnded -= EnableNextTrigger;
    }
}