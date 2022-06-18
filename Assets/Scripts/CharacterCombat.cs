using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    public float attackDelay = .6f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opponentStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        // Called by playerInteract on LMB
        // Only attack after attack cooldown is 0, then reset the attack cooldown
        if (attackCooldown <= 0f)
        {
            if (targetStats != null)
            {
                opponentStats = targetStats;
            }

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
        }
    }

    public void AttackHitEvent()
    {
        // Called by animation events
        if (opponentStats != null)
        {
            opponentStats.TakeDamage(myStats.damage.GetValue());
            opponentStats = null;
        }
    }
}
