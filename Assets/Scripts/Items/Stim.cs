using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Stim")]
public class Stim : Equipment
{
    MonoBehaviourHelper helper;
    public int heal;
    public float healTime;

    public override void Use()
    {
        base.Use();
        helper = FindObjectOfType<MonoBehaviourHelper>();
        helper.equipmentScript = this;
        helper.Heal(heal, healTime);
    }

    public override void Unequip()
    {
        base.Unequip();
        StopUse();
    }

    private void StopUse() 
    {
        helper = FindObjectOfType<MonoBehaviourHelper>();
        helper.CancelHeal();
        inUse = false;
    }
}
