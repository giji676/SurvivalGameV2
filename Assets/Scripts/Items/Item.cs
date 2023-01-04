using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public ItemType itemType = ItemType.Item;
    public GameObject parent;
    
    public bool stackable = true;
    public int maxStack = 32;
    public int currentStack = 1;
    public virtual void Equip()
    {

    }

    public virtual void Unequip()
    {

    }

    public void RemoveFromInventory()
    {
        //Inventory.instance.Remove(this);
    }
}

public enum ItemType
{
    Armor,
    Tool,
    Weapon,
    Item
}
