using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    [Header("Настройки квеста")]
    public TMP_InputField inputField;
    public TMP_Text feedbackText;
    public Button confirmButton;
    public string correctAnswer = "unity";
    
    [Header("Объекты для управления")]
    [Tooltip("Объекты, которые нужно выключить при правильном ответе")]
    public GameObject[] objectsToDisable;
    
    [Tooltip("Необязательно: объект, который нужно включить при правильном ответе")]
    public GameObject objectToEnable;     
    
    public movee playerMovement;

    private void Start()
    {
        gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
        confirmButton.onClick.AddListener(CheckAnswer);
        
        // Выключаем объект при старте, если он задан
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(false);
        }
    }

    public void ShowQuest()
    {
        playerMovement.LockMovement(true);
        gameObject.SetActive(true);
        inputField.text = "";
        feedbackText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void HideQuest()
    {
        playerMovement.LockMovement(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void CheckAnswer()
    {
        string userInput = inputField.text.Trim().ToLower();
        bool isCorrect = userInput == correctAnswer.ToLower();

        feedbackText.text = isCorrect ? "Правильно!" : "Неправильно!";
        feedbackText.gameObject.SetActive(true);

        if (isCorrect)
        {
            // Выключаем указанные объекты
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null) obj.SetActive(false);
            }
            
            // Включаем нужный объект, если он указан
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
            
            HideQuest();
        }
    }
}