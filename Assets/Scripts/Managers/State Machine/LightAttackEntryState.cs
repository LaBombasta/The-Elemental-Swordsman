using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackEntryState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        AssignAttackValues(0);
       
        attackType.Equals(AttackType.Light);
        attackIndex = 1;
        animator.SetTrigger(attackType.ToString() + "Attack" + attackIndex);
        magicSystem.FirstAttack(attackType.ToString());
        //Debug.Log("Light attack Entry State");
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
                stateMachine.SetNextState(new LightAttackImbueState());
            }
            if (canComboH)
            {
                stateMachine.SetNextState(new HeavyAttackImbueState());
            }
        }
        if (fixedtime>=duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}
