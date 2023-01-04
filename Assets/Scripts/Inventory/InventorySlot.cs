using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public InventoryItem inventoryItem;

    [SerializeField]
    private TextMeshProUGUI stackText;

    Inventory inventory;
    Armor armor;
    Hotbar hotbar;

    private void Start()
    {
        inventory = Inventory.instance;
        armor = Armor.instance;
        hotbar = Hotbar.instance;

        // if (item.currentStack > 1)
        //     stackText.text = item.currentStack.ToString();
        // else
        //     stackText.text = "";
    }

    public void AddItem(InventoryItem newInventoryItem)
    {
        inventoryItem = newInventoryItem;
        icon.sprite = inventoryItem.item.icon;
        icon.enabled = true;

        if (inventoryItem.currentStack > 1)
            stackText.text = inventoryItem.currentStack.ToString();
        else
            stackText.text = "";
    }

    public void ClearSlot()
    {
        inventoryItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton()
    {
        //Inventory.instance.Remove(item);
    }

    /*
    public void EquipItem()
    {
        if (item != null)
        {
            item.Equip();
        }
    }
    */
    /*

    public void HotbarTransfer()
    {
        if (item != null)
        {
            if (armor.Add(item))
            {
                hotbar.Remove(item);
            }

            else if (inventory.Add(item))
            {
                hotbar.Remove(item);
            }
        }
    }

    public void InventoryTransfer()
    {
        if (item != null)
        {
            if (armor.Add(item))
            {
                inventory.Remove(item);
            }

            else if (hotbar.Add(item))
            {
                inventory.Remove(item);
            }
        }
    }

    public void ArmorTransfer()
    {
        if (item != null)
        {
            if (inventory.Add(item))
            {
                armor.Remove(item);
            }

            else if (hotbar.Add(item))
            {
                armor.Remove(item);
            }
        }
    }
    */
}
