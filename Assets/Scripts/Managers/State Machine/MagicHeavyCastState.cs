using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHeavyCastState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        CheckCombo();
        AssignAttackValues(7);
        myCharacter.perfectPress = false;
        animator.SetTrigger("MagicHeavy");
        //Debug.Log("Magic Heavy State");
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
