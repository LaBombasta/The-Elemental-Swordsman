using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackMagicState : AttackBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        AssignAttackValues(2);
        attackType = AttackType.Light;
        attackIndex = 3;
        animator.SetTrigger(attackType.ToString() + "Attack" + attackIndex);
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
}
