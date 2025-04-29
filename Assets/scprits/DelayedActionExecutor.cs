using UnityEngine;
using System.Collections;

// Помести этот скрипт на префаб или объект игрока
public class DelayedActionExecutor : MonoBehaviour
{
    // Метод, который вызывается из QuestAdvanceItem
    public void ExecuteDelayedTeleportAndQuest(float delay, Vector3 targetPosition, GameObject triggerToActivate)
    {
        StartCoroutine(TeleportAndQuestSequence(delay, targetPosition, triggerToActivate));
    }

    private IEnumerator TeleportAndQuestSequence(float delay, Vector3 targetPosition, GameObject triggerToActivate)
    {
        Debug.Log($"Начинается задержка {delay} сек перед телепортацией...");

        // Ждем указанное количество секунд
        yield return new WaitForSeconds(delay);

        Debug.Log("Задержка окончена. Телепортация...");

        // Телепортируем игрока (объект, на котором висит этот скрипт)
        transform.position = targetPosition;

        Debug.Log("Телепортация завершена. Активация триггера и квеста...");

        // Активируем триггер, если он есть
        if (triggerToActivate != null)
        {
            triggerToActivate.SetActive(true);
            Debug.Log($"Триггер '{triggerToActivate.name}' активирован.");
        }

        // Продвигаем квест, если QuestManager существует
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.TriggerNextQuest();
            Debug.Log("QuestManager.Instance.TriggerNextQuest() вызван.");
        }
        else
        {
             Debug.LogWarning("QuestManager.Instance не найден. Квест не продвинут.");
        }
    }
}