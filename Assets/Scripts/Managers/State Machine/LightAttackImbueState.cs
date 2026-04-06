using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackImbueState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        CheckCombo();
        AssignAttackValues(1);
        myCharacter.perfectPress = false;
        attackType.Equals(AttackType.Light);
        attackIndex = 2;
        animator.SetTrigger(attackType.ToString() + "Attack" + attackIndex);
        magicSystem.SecondAttack(attackType.ToString());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (animator.GetFloat("ComboWindow")>.01f)
        {
            if(canComboN)
            {
                stateMachine.SetNextState(new MagicPreparationState());
            }
            if (canComboL)
            {
                stateMachine.SetNextState(new LightAttackMagicState());
            }
            if (canComboH)
            {
                stateMachine.SetNextState(new HeavyAttackMagicState());
            }
        }
       
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
        
    }
}
