using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public ItemType itemType = ItemType.Item;
    public int maxStack = 1;

    public virtual void Use()
    {

    }

    public virtual void StopUse()
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
