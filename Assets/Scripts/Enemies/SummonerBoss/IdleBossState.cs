using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBossState : State
{
    protected Animator animator;
    public override void OnEnter(StateMachine _stateMachine)
    {
        
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        
    }
}
