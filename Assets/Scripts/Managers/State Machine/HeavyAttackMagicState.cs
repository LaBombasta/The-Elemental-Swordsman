using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackMagicState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        CheckCombo();
        AssignAttackValues(5);
        myCharacter.perfectPress = false;
        attackType = AttackType.Heavy;
        attackIndex = 3;
        animator.SetTrigger(attackType.ToString() + "Attack" + attackIndex);
        //remove later this is for testing;
        magicSystem.ParticlePulse();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
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
