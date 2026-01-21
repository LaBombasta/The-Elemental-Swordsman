using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunState : State
{
    protected Animator animator;
    protected float duration = 7f;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //Debug.Log("Im Stunned");
        animator = GetComponent<Animator>();
        animator.SetTrigger("Stunned");

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }

    public override void OnExit()
    {
        base.OnExit();

    }
}

