using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicNoPreparationState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        AssignAttackValues(9);
        animator.SetTrigger("NoPreparation");
        //Debug.Log("Magic Preparation State");
    }

    public override void OnUpdate()
    {

        base.OnUpdate();
        if (animator.GetFloat("ComboWindow") > .01f)
        {
            Debug.Log(stateMachine.previousState);
            if (canComboN)
            {
                stateMachine.SetNextState(new MagicNoPreparationState());
            }
            if (stateMachine.previousState.GetType() == typeof(LightAttackEntryState) || stateMachine.previousState.GetType() == typeof(HeavyAttackEntryState))
            {
                if (canComboL)
                {
                    stateMachine.SetNextState(new LightAttackImbueState());
                }
                if (canComboH)
                {
                    stateMachine.SetNextState(new HeavyAttackImbueState());
                }
            }
            else
            {
                if (canComboL)
                {
                    stateMachine.SetNextState(new LightAttackEntryState());
                }
                if (canComboH)
                {
                    stateMachine.SetNextState(new HeavyAttackEntryState());
                }
            }
            
        }
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}
