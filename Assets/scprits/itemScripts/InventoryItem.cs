using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public bool removeAfterUse;

    public virtual void Use()
    {
    }
}
