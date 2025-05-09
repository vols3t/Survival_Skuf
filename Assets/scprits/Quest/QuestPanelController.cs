using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text feedbackText;
    public Button confirmButton;
    public string correctAnswer = "unity";
    public GameObject[] objectsToDisable;

    private void Start()
    {
        feedbackText.gameObject.SetActive(false);
        confirmButton.onClick.AddListener(CheckAnswer);
    }

    private void CheckAnswer()
    {
        string userInput = inputField.text.Trim().ToLower();
        bool isCorrect = userInput == correctAnswer.ToLower();

        feedbackText.text = isCorrect ? "Правильно!" : "Неправильно!";
        feedbackText.gameObject.SetActive(true);

        if (isCorrect)
        {
            if (objectsToDisable != null && objectsToDisable.Length > 0)
            {
                foreach (GameObject obj in objectsToDisable)
                {
                    if (obj != null) obj.SetActive(false);
                }
            }

            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}