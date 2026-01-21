using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLightCastState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        AssignAttackValues(8);
        animator.SetTrigger("MagicLight");
        Debug.Log("Magic Light State");
    }

    public override void OnUpdate()
    {

        base.OnUpdate();
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }

    }
}
