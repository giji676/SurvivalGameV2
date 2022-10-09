using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Item item;

    int maxStack;
    int currentStack = 0;

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

        if (currentStack >1)
            stackText.text = currentStack.ToString();
        else
            stackText.text = "";
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        maxStack = item.maxStack;
        currentStack++;

        if (currentStack > 1)
            stackText.text = currentStack.ToString();
        else
            stackText.text = "";
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        currentStack = 0;
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
}
