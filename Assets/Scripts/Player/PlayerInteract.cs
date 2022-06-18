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

    private Interactable interactable;

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
        characterCombat = GetComponent<CharacterCombat>();
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);

                if (inputManager.onFoot.LMB.triggered)
                {
                    if (interactable.gameObject.tag == "Enemy")
                    {
                        interactable.BaseInteract();
                    }
                    else
                    {
                        characterCombat.Attack(null);
                    }
                }
                else if (inputManager.onFoot.Interact.triggered)
                {
                    if (interactable.gameObject.tag != "Enemy")
                    {
                        interactable.BaseInteract();
                    }
                }
            }
        }
        else if(inputManager.onFoot.LMB.triggered)
        {
            characterCombat.Attack(null);
        }
    }
}
