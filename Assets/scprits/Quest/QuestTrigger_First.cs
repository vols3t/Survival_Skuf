using UnityEngine;

public class QuestTrigger_First : MonoBehaviour
{
    public GameObject questUI;         
    public GameObject inputFieldObj;   
    public GameObject answerButton;   
    
    private bool playerInRange = false;

    void Start()
    {
        questUI.SetActive(false);
        inputFieldObj.SetActive(false);
        answerButton.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            questUI.SetActive(true);
            inputFieldObj.SetActive(true);
            answerButton.SetActive(true);
            Time.timeScale = 0f;
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