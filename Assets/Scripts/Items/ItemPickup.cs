using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

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
        return Inventory.instance.Add(item);
    }

    bool TryHotbarPickUp()
    {
        return Hotbar.instance.Add(item);
    }
}
