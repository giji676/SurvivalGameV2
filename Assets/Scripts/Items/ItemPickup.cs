using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interacrable
{
    public Item item;

    protected override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        //Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) 
            Destroy(gameObject);
    }
}
