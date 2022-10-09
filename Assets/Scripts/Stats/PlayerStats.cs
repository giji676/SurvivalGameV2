using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    PlayerHealth playerHealth;
    InputManager inputManager;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        inputManager = GetComponent<InputManager>();
        //EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void Update()
    {
        if (inputManager.interaction.TakeDamage.triggered)
            TakeDamage(10);

        if (inputManager.interaction.Heal.triggered)
            RestoreHealth(10);

        playerHealth.newHealth = currentHealth;
        playerHealth.maxHealth = maxHealth;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
