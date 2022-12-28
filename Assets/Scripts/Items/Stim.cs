using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Stim")]
public class Stim : Equipment
{
    MBStim mbStim;
    public int heal;
    public float timeDelay;

    public bool inUse;

    public void Use()
    {
        mbStim = FindObjectOfType<MBStim>();
        mbStim.stim = this;
        mbStim.Heal(heal, timeDelay);
    }

    public override void Unequip()
    {
        base.Unequip();
        StopUse();
    }

    private void StopUse() {
        mbStim = FindObjectOfType<MBStim>();
        mbStim.CancelHeal();
        inUse = false;
    }
}
