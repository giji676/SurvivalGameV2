using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Tool")]
public class Tool : Equipment
{
    MonoBehaviourHelper helper;
    public int damage;
    public float attackSpeed;
    public float range;

    public override void Use() 
    {
        base.Use();
        helper = FindObjectOfType<MonoBehaviourHelper>();
        helper.equipmentScript = this;
        helper.ToolAttack(damage, attackSpeed, range);
    }

    public override void Unequip()
    {
        base.Unequip();
        StopUse();
    }

    private void StopUse() 
    {
        helper = FindObjectOfType<MonoBehaviourHelper>();
        helper.CancelToolAttack();
        inUse = false;
    }
}
