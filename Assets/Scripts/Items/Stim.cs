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

    public void Heal()
    {
        mbStim = FindObjectOfType<MBStim>();
        mbStim.stim = this;
        mbStim.Heal(heal, timeDelay);
    }

    public override void StopUse()
    {
        base.StopUse();
        mbStim = FindObjectOfType<MBStim>();
        mbStim.CancelHeal();
        inUse = false;
    }
}
