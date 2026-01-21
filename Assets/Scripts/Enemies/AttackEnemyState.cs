using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackType.Equals(AttackType.Light);
        attackIndex = 1;
        AssignAttackValues(0);
        animator.SetTrigger("Attack" + attackIndex);
        //Debug.Log("Enemy Attack Entry State");
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
        //animator.SetTrigger("RevertToIdle");
    }
}
