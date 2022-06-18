using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private InputManager inputManager;
    private Vector3 playerVelocity;
    private bool isGrounded;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float smoothInputSpeed = 0.12f;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    public GameObject inventoryUI;
    public EquipmentManager equipmentManager;

    public Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (inputManager.onFoot.Inventory.triggered)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = !Cursor.visible;
        }

        if (inputManager.onFoot.Unequip.triggered)
        {
            equipmentManager.UnequipAll();
        }
    }

    public void ProcessMove(Vector2 input, float run)
    {
        moveDirection = Vector3.zero;

        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        moveDirection.x = currentInputVector.x;
        moveDirection.z = currentInputVector.y;

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessMB(float LMB, float RMB)
    {
        //Debug.Log(LMB.ToString() + RMB.ToString());
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
