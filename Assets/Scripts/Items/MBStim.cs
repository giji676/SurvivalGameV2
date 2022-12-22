using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBStim : MonoBehaviour
{
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void Heal(int point, float time)
    {
        StartCoroutine(HealCoroutine(point, time));
    }

    private IEnumerator HealCoroutine(int point, float time)
    {
        yield return new WaitForSeconds(time);
        playerStats.RestoreHealth(point);
    }
}
