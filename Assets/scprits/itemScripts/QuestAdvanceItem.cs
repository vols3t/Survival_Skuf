using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Quest Advance Item")]
public class QuestAdvanceItem : InventoryItem
{
    public GameObject triggerToActivate; 
    public override void Use()
    
    {
        base.Use();

        if (triggerToActivate != null)
            triggerToActivate.SetActive(true);
        
        if (QuestManager.Instance != null)
            QuestManager.Instance.TriggerNextQuest();
    }
}