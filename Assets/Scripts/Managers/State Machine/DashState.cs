using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        CheckCombo();
        AssignAttackValues(10);
        myCharacter.perfectPress = false;
        animator.SetTrigger("Dash");
        Debug.Log(animator.GetFloat("ActiveAttack"));
        Debug.Log(animator.GetFloat("ActiveStep"));
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
