using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    private InventoryItem inventoryItem;

    protected override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        if (TryHotbarPickUp())
        {
            Destroy(gameObject);
        }
        else
        {
            if (TryInventoryPickUp())
            {
                Destroy(gameObject);
            }
        }
    }

    bool TryInventoryPickUp()
    {
        inventoryItem = new InventoryItem(item);
        return Inventory.instance.Add(inventoryItem);
    }

    bool TryHotbarPickUp()
    {
        inventoryItem = new InventoryItem(item);
        return Hotbar.instance.Add(inventoryItem);
    }
}
