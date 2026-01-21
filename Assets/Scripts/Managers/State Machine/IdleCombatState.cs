using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterStats))]
public class IdleCombatState : State
{
    private CharacterStats playerStats;
    protected Animator animator;
    protected Rigidbody2D rb;
    private Vector2 movement;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<CharacterStats>();
        //Debug.Log("Entering Idle State");
        
    }


    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb.velocity = movement * playerStats.MoveSpeed;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }
        //Debug.Log("working and updating");
        
    }
    public override void OnExit()
    {
        base.OnExit();
        //ZeroOutMovement();
    }
    protected void ZeroOutMovement()
    {
        rb.velocity = Vector2.zero;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
    }
}
