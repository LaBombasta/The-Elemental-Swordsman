using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPreparationState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        CheckCombo();
        AssignAttackValues(6);
        myCharacter.perfectPress = false;
        animator.SetTrigger("MagicPreparation");
        //Debug.Log("Magic Preparation State");
    }

    public override void OnUpdate()
    {

        base.OnUpdate();
        if (animator.GetFloat("ComboWindow") > .01f)
        {
            if (canComboN)
            {
                stateMachine.SetNextState(new MagicNoPreparationState());
            }
            if (canComboL)
            {
                stateMachine.SetNextState(new MagicLightCastState());
            }
            if (canComboH)
            {
                stateMachine.SetNextState(new MagicHeavyCastState());
            }
        }
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
        
    }
}
