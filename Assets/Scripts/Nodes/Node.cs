using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Interactable
{
    public float health = 100f;
    public Item item;

    protected override void Interact()
    {
        base.Interact();
    }

    public InventoryItem Mine()
    {
        health -= 1f;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.localScale *= 0.92f;
        return new InventoryItem(item);
    }
}
