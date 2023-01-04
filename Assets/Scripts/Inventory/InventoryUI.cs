using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryParent;
    public Transform armorParent;
    public Transform hotbarParent;
    public GameObject inventoryUI;

    Inventory inventory;
    Armor armor;
    Hotbar hotbar;

    InventorySlot[] inventorySlots;
    InventorySlot[] armorSlots;
    InventorySlot[] hotbarSlots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;

        armor = Armor.instance;
        armor.onItemChangedCallback += UpdateArmorUI;

        hotbar = Hotbar.instance;
        hotbar.onItemChangedCallback += UpdateHotbarUI;

        inventorySlots = inventoryParent.GetComponentsInChildren<InventorySlot>();
        armorSlots = armorParent.GetComponentsInChildren<InventorySlot>();
        hotbarSlots = hotbarParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.inventoryItems.Count)
            {
                inventorySlots[i].ClearSlot();
                inventorySlots[i].AddItem(inventory.inventoryItems[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }

    void UpdateArmorUI()
    {
        for (int i = 0; i < armorSlots.Length; i++)
        {
            if (i < armor.inventoryItems.Count)
            {
                armorSlots[i].ClearSlot();
                armorSlots[i].AddItem(armor.inventoryItems[i]);
            }
            else
            {
                armorSlots[i].ClearSlot();
            }
        }
    }

    void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < hotbar.inventoryItems.Count)
            {
                hotbarSlots[i].ClearSlot();
                hotbarSlots[i].AddItem(hotbar.inventoryItems[i]);
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }
    }
}
