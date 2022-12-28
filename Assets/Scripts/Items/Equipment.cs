using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;
    public bool inUse;

    public int armorModifier;
    public int damageModifier;

    public override void Equip()
    {
        base.Equip();
        EquipmentManager.instance.Equip(this);
    }

    public override void Unequip()
    {
        base.Unequip();
        EquipmentManager.instance.UnequipItem(this);
    }

    public virtual void Use() 
    {
        Debug.Log("Using " + name);
    }
}


public enum EquipmentSlot {
    Head,
    Chest,
    Legs,
    Weapon,
    Shield,
    Feet
}

public enum EquipmentMeshRegion
{
    Legs,
    Arms,
    Torso
}