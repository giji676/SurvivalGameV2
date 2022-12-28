using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    private PlayerAnimator playerAnimator;
    private CharacterCombat characterCombat;

    Hotbar hotbar;

    private Interactable interactable;

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
        characterCombat = GetComponent<CharacterCombat>();
        hotbar = Hotbar.instance;
    }

    private void Update()
    {
        // Raycast each update
        playerUI.UpdateText(string.Empty);
        PlayerInteractRay();

        // If Left Mouse Button is pressed (attack)
        if (inputManager.interaction.LMB.triggered)
        {
            if (hotbar.activeSlot > -1)
            {
                Equipment equipment = (Equipment)hotbar.items[hotbar.activeSlot];

                if (!equipment.inUse) 
                {
                    equipment.Use();
                    characterCombat.Attack(null);

                    if (equipment.itemType == ItemType.Tool) 
                    {
                        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                        Debug.DrawRay(ray.origin, ray.direction * distance);
                        RaycastHit hitInfo; 
                        
                        if (Physics.Raycast(ray, out hitInfo, distance, mask))
                        {
                            if (hitInfo.collider.GetComponent<Interactable>() != null)
                            {
                                interactable = hitInfo.collider.GetComponent<Interactable>();
                                playerUI.UpdateText(interactable.promptMessage);
                                {
                                    if (interactable.gameObject.tag == "Node")
                                    {
                                        Node hitObject = hitInfo.collider.gameObject.GetComponent<Node>();
                                        Inventory.instance.Add(hitObject.Mine());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void PlayerInteractRay() 
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // If the object hit is an Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);

                if (inputManager.interaction.Interact.triggered)
                {
                    // If the object is an interactable, and E is pressed (interact)
                    if (interactable.gameObject.tag != "Enemy")
                    {
                        interactable.BaseInteract();
                    }
                }
            }
        }
        else if(inputManager.interaction.LMB.triggered)
        {
            // If the raycast didn't hit anything, still play the attack animation
            characterCombat.Attack(null);
        }
    }
}
