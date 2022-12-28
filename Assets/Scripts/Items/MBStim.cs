using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBStim : MonoBehaviour
{
    PlayerStats playerStats;
    IEnumerator healCoroutine;
    public Stim stim; // Heal and CancelHeal called from this stim script

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void Heal(int point, float time)
    {
        healCoroutine = HealCoroutine(point, time);
        StartCoroutine(healCoroutine);
    }

    public void CancelHeal()
    {
        StopCoroutine(healCoroutine);
        stim.inUse = false;
    }

    private IEnumerator HealCoroutine(int point, float time)
    {
        stim.inUse = true;
        yield return new WaitForSeconds(time);
        playerStats.RestoreHealth(point);
        stim.inUse = false;
    }
}
