using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("UI References")]
    public GameObject panel;
    public List<Button> slotButtons;
    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text itemDescription;
    public Button useButton;
    public Canvas fadePanel; 
    public Image fadeImage;   
    
    [Header("Inventory Settings")]
    private const int INVENTORY_SIZE = 4;
    private InventoryItem[] items = new InventoryItem[INVENTORY_SIZE];
    private int selectedIndex = -1;
    private int occupiedSlots = 0;

    void Start()
    {
        Instance = this;
        panel.SetActive(false);
        useButton.onClick.AddListener(UseSelectedItem);
        InitializeSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < slotButtons.Count; i++)
        {
            UpdateSlotUI(i);
        }
    }

    private void ToggleInventory()
    {
        bool isActive = !panel.activeSelf;
        panel.SetActive(isActive);
        Time.timeScale = isActive ? 0 : 1;

        if (fadePanel != null){
            fadePanel.sortingOrder = 100;
        }
            

        if (!isActive)
        {   
            fadePanel.sortingOrder = 0;
            ClearSelection();
        }
    }

    public bool AddItem(InventoryItem newItem)
    {
        if (occupiedSlots >= INVENTORY_SIZE)
        {
            Debug.LogWarning("Inventory is full!");
            return false;
        }

        items[occupiedSlots] = newItem;
        occupiedSlots++;
        UpdateSlotUI(occupiedSlots - 1);
        return true;
    }

    private void UpdateSlotUI(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slotButtons.Count) return;

        var hasItem = slotIndex < occupiedSlots && items[slotIndex] != null;
        var slot = slotButtons[slotIndex];
        slot.gameObject.SetActive(hasItem);

        if (hasItem)
        {
            var slotText = slot.GetComponentInChildren<TMP_Text>();
            if (slotText != null)
                slotText.text = items[slotIndex].itemName;

            slot.onClick.RemoveAllListeners();
            var indexCopy = slotIndex;
            slot.onClick.AddListener(() => SelectItem(indexCopy));
        }
    }

    private void SelectItem(int index)
    {
        if (index < 0 || index >= occupiedSlots || items[index] == null) return;

        selectedIndex = index;
        var item = items[index];

        itemIcon.sprite = item.icon;
        itemIcon.color = Color.white;
        itemNameText.text = item.itemName;
        itemDescription.text = item.description;
        useButton.gameObject.SetActive(true);
    }

    private void UseSelectedItem()
    {
        if (selectedIndex < 0 || selectedIndex >= occupiedSlots || items[selectedIndex] == null)
            return;

        items[selectedIndex].Use();

        for (int i = selectedIndex; i < occupiedSlots - 1; i++)
        {
            items[i] = items[i + 1];
            UpdateSlotUI(i);
        }

        items[occupiedSlots - 1] = null;
        occupiedSlots--;
        UpdateSlotUI(occupiedSlots);

        ClearSelection();

        panel.SetActive(false);
        fadePanel.sortingOrder = 0;
        Time.timeScale = 1;
    }

    private void ClearSelection()
    {
        selectedIndex = -1;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        itemNameText.text = "";
        itemDescription.text = "";
        useButton.gameObject.SetActive(false);
    }
}
