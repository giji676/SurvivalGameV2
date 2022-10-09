using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.MovementActions movement;
    public PlayerInput.InteractionActions interaction;


    private PlayerMotor motor;
    private PlayerLook look;

    private Hotbar hotbar;

    private void Awake()
    {
        playerInput = new PlayerInput();
        movement = playerInput.Movement;
        interaction = playerInput.Interaction;

        hotbar = Hotbar.instance;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        movement.Jump.performed += ctx => motor.Jump();
    }

    private void Update()
    {
        if (interaction.Inventory.triggered)
            motor.InventoryTrigger();

        if (interaction.Unequip.triggered)
            motor.UnequipAll();

        #region Hotbar slots

        if (interaction.HB0.triggered)
            hotbar.UseSlot(0);

        if (interaction.HB1.triggered)
            hotbar.UseSlot(1);

        if (interaction.HB2.triggered)
            hotbar.UseSlot(2);

        if (interaction.HB3.triggered)
            hotbar.UseSlot(3);

        if (interaction.HB4.triggered)
            hotbar.UseSlot(4);

        if (interaction.HB5.triggered)
            hotbar.UseSlot(5);

        #endregion
    }

    private void FixedUpdate()
    {
        motor.ProcessMove(movement.Movement.ReadValue<Vector2>(), movement.Run.ReadValue<float>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(movement.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        movement.Enable();
        interaction.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        interaction.Disable();
    }
}
