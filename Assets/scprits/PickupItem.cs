using UnityEngine;

public class PickupItem : MonoBehaviour
{   
    public bool NeededDestroy;
    public InventoryItem itemToPickup; 
    private bool canPickup = false;    

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.F))
        {
            bool added = InventoryUI.Instance.AddItem(itemToPickup);
            Debug.Log("Item pickup attempted. Success: " + added);

            if (added && NeededDestroy)
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
