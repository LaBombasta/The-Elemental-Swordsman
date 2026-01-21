using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackImbueState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        AssignAttackValues(4);
        attackType = AttackType.Heavy;
        attackIndex = 2;
        animator.SetTrigger(attackType.ToString() + "Attack" + attackIndex);
        magicSystem.SecondAttack(attackType.ToString());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (animator.GetFloat("ComboWindow") > .01f)
        {
            if (canComboN)
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
    
    public override void Nudge(float stepSpeed)
    {
        if (animator.GetFloat("ActiveStep") > 0f)
        {
            Vector2 dir = new Vector2(InputManager.Movement.x, InputManager.Movement.y);
            rb.velocity = dir * stepSpeed;
        }
        else
        {
            ZeroOutMovement();
        }
    }
}

