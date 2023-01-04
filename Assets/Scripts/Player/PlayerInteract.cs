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
    private PlayerStats playerStats;

    Hotbar hotbar;

    private Interactable interactable;
    
    private IEnumerator cooldownCoroutine;
    private IEnumerator actionCoroutine;
    Equipment equipment;

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
        characterCombat = GetComponent<CharacterCombat>();
        playerStats = GetComponent<PlayerStats>();
        hotbar = Hotbar.instance;
        hotbar.onHotbarUseCallBack += StopCoroutines;
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
                equipment = (Equipment)hotbar.inventoryItems[hotbar.activeSlot].item;

                if (!equipment.inUse) 
                {
                    equipment.Use();
                    characterCombat.Attack(null);

                    if (equipment is Tool tool) 
                    {
                        cooldownCoroutine = Cooldown(equipment, tool.attackSpeed);
                        actionCoroutine = ToolAttackCoroutine(tool);
                        StartCoroutine(cooldownCoroutine);
                        StartCoroutine(actionCoroutine);
                    }

                    if (equipment is Stim stim) 
                    {
                        cooldownCoroutine = Cooldown(equipment, stim.totalTime);
                        actionCoroutine = StimCoroutine(stim);
                        StartCoroutine(cooldownCoroutine);
                        StartCoroutine(actionCoroutine);
                    }
                }
            }
        }
    }
    
    private IEnumerator ToolAttackCoroutine(Tool tool) 
    {
        yield return new WaitForSeconds(tool.hitTime);
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * tool.range);
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

    private IEnumerator StimCoroutine(Stim stim) 
    {
        yield return new WaitForSeconds(stim.healTime);
        playerStats.RestoreHealth(stim.heal);
    }
    private IEnumerator Cooldown(Equipment equipment, float time) 
    {
        equipment.inUse = true;
        yield return new WaitForSeconds(time);
        equipment.inUse = false;
    }

    private void StopCoroutines() 
    {
        if (cooldownCoroutine != null)
            StopCoroutine(cooldownCoroutine);
        if (actionCoroutine != null)
            StopCoroutine(actionCoroutine);
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
