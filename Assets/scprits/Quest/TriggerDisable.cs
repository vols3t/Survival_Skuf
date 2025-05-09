using UnityEngine;
using System.Collections;

public class QuestDisabler : MonoBehaviour


{
    public GameObject objectToDisable; // <- Назначаешь вручную в инспекторе

    
    
    public void TriggerDisable()
    {
        Debug.Log("TriggerDisable вызван");
        StartCoroutine(DisableAfterSeconds(5f));
    }

    private IEnumerator DisableAfterSeconds(float seconds)
    {
        Debug.Log("Ждём " + seconds + " секунд...");
        yield return new WaitForSeconds(seconds);

        if (objectToDisable != null)
        {
            Debug.Log("Отключаем объект: " + objectToDisable.name);
            objectToDisable.SetActive(false);
        }
        else
        {
            Debug.LogWarning("objectToDisable НЕ назначен!");
        }
    }

}