using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton()
    {
        //Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void HotbarToInventory()
    {
        if (item != null)
        {
            if (Inventory.instance.Add(item))
            {
                Hotbar.instance.Remove(item);
            }
        }
    }

    public void InventoryToHotbar()
    {
        if (item != null)
        {
            if (Hotbar.instance.Add(item))
            {
                Inventory.instance.Remove(item);
            }
        }
    }
}
