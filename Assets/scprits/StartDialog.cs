using System;
using UnityEngine;
using DialogueEditor;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ConversationManager.Instance != null && myConversation != null)
            {
                ConversationManager.Instance.StartConversation(myConversation);
                GetComponent<Collider2D>().enabled = false; // Отключаем триггер
            }
        }
    }
}
