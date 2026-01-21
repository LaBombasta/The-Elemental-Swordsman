using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBossState : State
{
    protected Animator animator;
    protected float duration = .675f;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        animator.SetTrigger("Teleport");

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
