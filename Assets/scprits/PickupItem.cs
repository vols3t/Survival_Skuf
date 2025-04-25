using UnityEngine;

public class PickupItem : MonoBehaviour
{   
    public bool NeededDestroy;
    public InventoryItem itemToPickup; 
    private bool canPickup = false;    

    void Update()
    {
        // Если игрок в зоне действия предмета и нажимает клавишу F
        if (canPickup && Input.GetKeyDown(KeyCode.F))
        {
            InventoryManager.Instance.AddItem(itemToPickup);
            if (NeededDestroy)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }
}
