using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    #region Singleton

    public static Armor instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of armor found!");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int inventorySpace = 5;
    public List<Item> items = new List<Item>();


    public bool Add(Item item)
    {
        if (items.Count < inventorySpace)
        {

            if (item.itemType == ItemType.Armor)
            {
                items.Add(item);
                item.Use();

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

                return true;
            }
        }

        return false;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
