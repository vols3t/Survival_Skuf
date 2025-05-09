using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorTaskSystem2D : MonoBehaviour
{
    public GameObject taskPanel; // Панель с задачей (UI Canvas)
    public Image taskImage;     // Картинка с заданием (PNG)
    public TMP_InputField answerInput; // Поле для ввода ответа
    public string correctAnswer = "123"; // Правильный ответ (задай свой)
    public KeyCode interactionKey = KeyCode.F; // Клавиша активации (F)

    private bool isPlayerNear = false;

    void Start()
    {
        taskPanel.SetActive(false); // Скрываем панель при старте
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey)) // Проверяем нажатие F
        {
            taskPanel.SetActive(true);
            answerInput.text = ""; // Очищаем поле ввода
            Time.timeScale = 0f; // Останавливаем игру (опционально)
        }
    }

    // Проверка ответа (вызывается при нажатии кнопки "Проверить" в UI)
    public void CheckAnswer()
    {
        if (answerInput.text == correctAnswer)
        {
            taskPanel.SetActive(false);
            Destroy(gameObject); // Удаляем дверь (или можно сделать анимацию)
            Time.timeScale = 1f; // Возобновляем игру
        }
        else
        {
            Debug.Log("Неверно! Попробуй ещё раз.");
            // Можно добавить звук ошибки или визуальный эффект
        }
    }

    // 2D-триггеры (используем Collider2D)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            taskPanel.SetActive(false);
            Time.timeScale = 1f; // Возобновляем игру, если игрок ушёл
        }
    }
}