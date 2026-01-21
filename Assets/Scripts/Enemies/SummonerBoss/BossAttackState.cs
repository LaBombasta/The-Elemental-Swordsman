using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : State
{
    protected Animator animator;
    protected float duration = 3.5f;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //Debug.Log("Im Attacking");
        animator = GetComponent<Animator>();
        animator.SetTrigger("Attack1");

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
