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
            if (i < inventory.items.Count)
            {
                inventorySlots[i].ClearSlot();
                inventorySlots[i].AddItem(inventory.items[i]);
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
            if (i < armor.items.Count)
            {
                armorSlots[i].ClearSlot();
                armorSlots[i].AddItem(armor.items[i]);
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
            if (i < hotbar.items.Count)
            {
                hotbarSlots[i].ClearSlot();
                hotbarSlots[i].AddItem(hotbar.items[i]);
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }
    }
}
