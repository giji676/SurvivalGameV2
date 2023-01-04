using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    #region Singleton

    public static Armor instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of armor found!");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int inventorySpace = 5;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();


    public bool Add(InventoryItem newItem)
    {
        if (inventoryItems.Count < inventorySpace)
        {
            if (newItem.item.itemType == ItemType.Armor)
            {
                inventoryItems.Add(newItem);
                newItem.item.Equip();

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

                return true;
            }
        }

        return false;
    }

    public void Remove(InventoryItem newItem)
    {
        inventoryItems.Remove(newItem);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
