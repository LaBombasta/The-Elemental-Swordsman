using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackEntryState : AttackBaseState
{
    
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        CheckCombo();
        AssignAttackValues(3);
        myCharacter.perfectPress = false;
        attackType = AttackType.Heavy;
        attackIndex = 1;
        animator.SetTrigger(attackType.ToString() + "Attack" + attackIndex);
        magicSystem.FirstAttack(attackType.ToString());
    }

    public override void OnUpdate()
    {

        base.OnUpdate();
        if (animator.GetFloat("ComboWindow") > .01f)
        {
            //Debug.Log(pressed);
            CheckCombo();
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
        
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }

}