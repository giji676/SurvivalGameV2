using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Stim")]
public class Stim : Equipment
{
    MBStim mbStim;
    public int heal;
    public float timeDelay;

    public void Heal()
    {
        mbStim = FindObjectOfType<MBStim>();
        mbStim.Heal(heal, timeDelay);
    }
}
