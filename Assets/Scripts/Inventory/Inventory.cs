using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int inventorySpace = 24;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    
    public bool Add(InventoryItem newItem)
    {
        Debug.Log(newItem.currentStack);
        /*
        If item is stackable
            If item exists in the inventory:
                If item stack is less than maximum stack:
                    Add as much as you can:
                        Move onto another slot with the remainder stack
                Check another slot
            Add it to the inventory
        */
        if (newItem.item.stackable) // If item is stackable
        {
            foreach (InventoryItem inventoryItem in inventoryItems) // For every item in the inventory
            {
                if (inventoryItem.item.name == newItem.item.name) // If the inventory name matches item name
                {
                    if (inventoryItem.currentStack < inventoryItem.item.maxStack) // If the inventory item current stack is less than maximum stack
                    {
                        int emptyStackAvailable = newItem.item.maxStack - inventoryItem.currentStack; // How much more the item can stack in the inventory
                        if (newItem.currentStack <= emptyStackAvailable)  // If the items currentstack is less than what the inventory item can stack
                        {
                            inventoryItem.currentStack += newItem.currentStack; // Increase the inventory item current stack with empty stack available
                            
                            if (onItemChangedCallback != null)
                                onItemChangedCallback.Invoke();
                            
                            return true;
                        }
                        else 
                        { // If the items current stack is more than what the inventory item can stack
                            newItem.currentStack -= emptyStackAvailable; // Decrease the items current stack with the available
                            inventoryItem.currentStack += emptyStackAvailable; // Increase the inventory item stack with the available

                            if (onItemChangedCallback != null)
                                onItemChangedCallback.Invoke();
                        }
                    }
                }
            }
            if (newItem.currentStack <= newItem.item.maxStack) // If items current stack is less or equal to items max stack
            { 
                if (inventoryItems.Count < inventorySpace) // If there is more space in the inventory
                {
                    inventoryItems.Add(newItem); // Add it as a new item in items list

                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();

                    return true;
                }
            }
            else // If the current stack is more than items max stack
            {
                if (inventoryItems.Count < inventorySpace) // If there is more space in the inventory
                {
                    InventoryItem newInventoryItem = newItem;
                    newInventoryItem.currentStack = newItem.item.maxStack;
                    newItem.currentStack -= newItem.item.maxStack;
                    inventoryItems.Add(newItem);

                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();
                    
                    Add(newItem);
                }

            }
        }

        if (inventoryItems.Count < inventorySpace)
        {
            inventoryItems.Add(newItem);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }

        return false;
    }

    public void Remove(InventoryItem inventoryItem)
    {
        inventoryItems.Remove(inventoryItem);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
