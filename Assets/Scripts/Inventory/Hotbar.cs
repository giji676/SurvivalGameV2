using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    #region Singleton

    public static Hotbar instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of hotbar found!");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public delegate void OnHotbarUse();
    public OnHotbarUse onHotbarUseCallBack;

    public int inventorySpace = 6;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public int activeSlot = -1;

    public bool Add(InventoryItem newItem)
    {
        if (inventoryItems.Count < inventorySpace)
        {
            inventoryItems.Add(newItem);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }

        return false;
    }

    public void Remove(InventoryItem newItem)
    {
        inventoryItems.Remove(newItem);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void UseSlot(int slot)
    {
        if (slot < inventoryItems.Count)
        {
            Item item = inventoryItems[slot].item;
            if (slot == activeSlot)
            {
                item.Unequip();
                if (item is Equipment equipment) 
                {
                    equipment.inUse = false;
                }
                activeSlot = -1;
            }

            else if (item.itemType == ItemType.Tool || item.itemType == ItemType.Weapon)
            {
                if (activeSlot != -1)
                {
                    inventoryItems[activeSlot].item.Unequip();
                }

                activeSlot = slot;
                item.Equip();
                if (item is Equipment equipment) 
                {
                    equipment.inUse = false;
                }
            }

            else if (item.itemType == ItemType.Armor)
            {
                if (Armor.instance.Add(inventoryItems[slot]))
                {
                    Remove(inventoryItems[slot]);
                }
            }
            if (onHotbarUseCallBack != null)
                onHotbarUseCallBack.Invoke();
        }
    }
}
