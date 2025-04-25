using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Image> slotImages;  
    public TMP_Text itemInfoText;   
    private List<InventoryItem> items = new List<InventoryItem>();
    private int selectedIndex = -1;

    void Awake()
    {
        Instance = this;
        UpdateInventoryUI();
    }

    public void AddItem(InventoryItem item)
    {
        if (items.Count < slotImages.Count)
        {
            items.Add(item);
            UpdateInventoryUI();
        }
    }

    public void UseSelectedItem()
    {
        if (selectedIndex >= 0 && selectedIndex < items.Count)
        {
            var item = items[selectedIndex];
            item.Use();

            if (item.removeAfterUse)
            {
                items.RemoveAt(selectedIndex);
                selectedIndex = -1;
                UpdateInventoryUI();
                ClearItemInfo();
            }
        }
    }

    public void ClearInventory()
    {
        items.Clear();
        selectedIndex = -1;
        UpdateInventoryUI();
        ClearItemInfo();
    }

    public void SelectSlot(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            selectedIndex = index;
            UpdateItemInfo();  
        }
    }

    private void UpdateInventoryUI()
{
    for (int i = 0; i < slotImages.Count; i++)
    {
        if (i < items.Count && items[i] != null)
        {
            slotImages[i].sprite = items[i].icon;
            
            float alpha = 1f;
            slotImages[i].color = new Color(1, 1, 1, alpha);
        }
        else
        {
            slotImages[i].sprite = null;
            slotImages[i].color = new Color(1, 1, 1, 0); 
        }
    }
}

    private void UpdateItemInfo()
    {
        if (selectedIndex >= 0 && selectedIndex < items.Count)
        {
            itemInfoText.text = $"{items[selectedIndex].itemName}\n{items[selectedIndex].description}";
        }
        else
        {
            itemInfoText.text = "Выберите предмет для информации";
        }
    }

    private void ClearItemInfo()
    {
        itemInfoText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);

        if (Input.GetKeyDown(KeyCode.E)) UseSelectedItem();
    }
}
