using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    protected Animator animator;
    PlayerMotor playerMotor;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("Velocity X", playerMotor.moveDirection.x);
        animator.SetFloat("Velocity Z", playerMotor.moveDirection.z);
    }
}
