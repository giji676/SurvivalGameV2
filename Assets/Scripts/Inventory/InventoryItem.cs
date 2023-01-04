using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public Item item;
    public int currentStack;

    public InventoryItem(Item item_) 
    {
        item = item_;
        currentStack++;
    }
}
