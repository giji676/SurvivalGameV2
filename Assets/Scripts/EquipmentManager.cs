using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Inventory inventory;
    Armor armor;
    Hotbar hotbar;

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    private void Start()
    {
        // Singleton instances of inventory, armor, and hotbar scripts
        inventory = Inventory.instance;
        armor = Armor.instance;
        hotbar = Hotbar.instance;

        // Get the number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate(newItem.mesh);

        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
    }

    public void UnequipItem(Equipment item)
    {
        int slotIndex = (int)item.equipSlot;
        Destroy(currentMeshes[slotIndex].gameObject);
        Equipment oldItem = currentEquipment[slotIndex];
        SetEquipmentBlendShapes(oldItem, 0);
        currentEquipment[slotIndex] = null;
        if (!oldItem.isDefaultItem && oldItem.itemType != ItemType.Tool && oldItem.itemType != ItemType.Weapon)
            inventory.Add(oldItem);
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        // Changed blend shape of a model to a weight, based on the item covered mesh regions
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
}
