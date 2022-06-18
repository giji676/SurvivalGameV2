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
            if (hitInfo.collider.GetComponent<Interacrable>() != null)
            {
                Interacrable interactable = hitInfo.collider.GetComponent<Interacrable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                    interactable.BaseInteract();
            }
        }
    }

    public void RunRaycast()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interacrable>() != null)
            {
                Interacrable interactable = hitInfo.collider.GetComponent<Interacrable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (interactable.gameObject.name == "Enemy")
                {
                    interactable.BaseInteract();
                }
                else
                {
                    characterCombat.Attack(null);
                }
            }
        }
        else
        {
            characterCombat.Attack(null);
        }
    }
}
