using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourHelper : MonoBehaviour
{
    PlayerStats playerStats;
    IEnumerator healCoroutine;
    IEnumerator toolAttackCoroutine;
    public Equipment equipmentScript;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void Heal(int point, float time)
    {
        healCoroutine = HealCoroutine(point, time);
        StartCoroutine(healCoroutine);
    }

    public void ToolAttack(int damage, float time, float range) 
    {
        toolAttackCoroutine = ToolAttackCoroutine(damage, time, range);
        StartCoroutine(toolAttackCoroutine);
    }

    public void CancelHeal()
    {
        StopCoroutine(healCoroutine);
        equipmentScript.inUse = false;
    }

    public void CancelToolAttack()
    {
        StopCoroutine(toolAttackCoroutine);
        equipmentScript.inUse = false;
    }

    private IEnumerator HealCoroutine(int point, float time)
    {
        equipmentScript.inUse = true;
        yield return new WaitForSeconds(time);
        playerStats.RestoreHealth(point);
        equipmentScript.inUse = false;
    }

    private IEnumerator ToolAttackCoroutine(int damage, float time, float range) 
    {
        equipmentScript.inUse = true;
        yield return new WaitForSeconds(time);
        equipmentScript.inUse = false;
    }
}
