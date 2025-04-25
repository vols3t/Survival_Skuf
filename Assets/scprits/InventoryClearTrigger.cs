using UnityEngine;

public class InventoryClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.ClearInventory();
            Destroy(gameObject);
        }
    }
}
