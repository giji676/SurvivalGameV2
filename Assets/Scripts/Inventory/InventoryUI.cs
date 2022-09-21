using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryParent;
    public Transform hotbarParent;
    public GameObject inventoryUI;

    Inventory inventory;
    Hotbar hotbar;

    InventorySlot[] inventorySlots;
    InventorySlot[] hotbarSlots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;

        hotbar = Hotbar.instance;
        hotbar.onItemChangedCallback += UpdateHotbarUI;

        inventorySlots = inventoryParent.GetComponentsInChildren<InventorySlot>();
        hotbarSlots = hotbarParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }

    void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < hotbar.items.Count)
            {
                hotbarSlots[i].AddItem(hotbar.items[i]);
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }
    }
}
