using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Quest Advance Item")]
public class QuestAdvanceItem : InventoryItem
{
    public override void Use()
    {
        QuestManager.Instance.TriggerNextQuest();
    }
}
