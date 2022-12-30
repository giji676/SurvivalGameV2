using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Tool")]
public class Tool : Equipment
{
    MonoBehaviourHelper helper;
    public int damage;
    public float attackSpeed; // How long the whole attack takes // Time from start to finish untill another attack can start
    public float hitTime; // When the ray will be cast to register the hit (0sec to attackSpeed)
    public float range;

    public override void Use() 
    {
        base.Use();
        // helper = FindObjectOfType<MonoBehaviourHelper>();
        // helper.equipmentScript = this;
        // helper.ToolAttack(damage, attackSpeed, range);
    }

    public override void Unequip()
    {
        base.Unequip();
        // StopUse();
    }

    private void StopUse() 
    {
        // helper = FindObjectOfType<MonoBehaviourHelper>();
        // helper.CancelToolAttack();
        // inUse = false;
    }
}
