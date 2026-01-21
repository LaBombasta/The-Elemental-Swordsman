using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected float duration { get; set; }
    protected Animator animator;
    protected CharacterStats playerStats;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        playerStats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    protected void ZeroOutMovement()
    {
        rb.velocity = Vector2.zero;
    }
    /*nudge function
     * bump the character in a certain direction
     * */

}