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

    public Item Mine()
    {
        health -= 10f;
        Debug.Log(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.localScale *= 0.95f;
        return item;
    }
}
