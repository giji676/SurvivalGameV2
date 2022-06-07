using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    PlayerMotor playerMotor;

    int velX = Animator.StringToHash("Velocity X");
    int velZ = Animator.StringToHash("Velocity Z");
    int usingCombatItem = Animator.StringToHash("UsingCombatItem");

    protected override void Start()
    {
        base.Start();
        playerMotor = GetComponent<PlayerMotor>();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    protected override void Update()
    {
        base.Update();
        animator.SetFloat(velX, playerMotor.moveDirection.x);
        animator.SetFloat(velZ, playerMotor.moveDirection.z);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            animator.SetBool(usingCombatItem, true);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            animator.SetBool(usingCombatItem, false);
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
            animator.SetBool(usingCombatItem, true);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0);
            animator.SetBool(usingCombatItem, false);
        }
    }
}
