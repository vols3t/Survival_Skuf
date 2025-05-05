using UnityEngine;
public class QuestAdvanceItem : InventoryItem
{
    public string targetObjectName; 

    public override void Use()
    {
        base.Use();

        GameObject obj = GameObject.Find(targetObjectName);
        if (obj != null)
        {
            var teleportScript = obj.GetComponent<TeleportOnTrigger>();
            if (teleportScript != null)
                teleportScript.SetActive(); 
        }

        QuestManager.Instance?.TriggerNextQuest();
    }
}
