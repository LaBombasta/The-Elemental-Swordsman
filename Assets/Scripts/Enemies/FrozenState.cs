using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenState : HurtState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator.SetTrigger("Frozen");
        duration = characterStats.GetStunTime();
        //AudioManager.instance.PlaySoundEffects(AudioManager.instance.EnemyHit);
        if (characterStats.team == TeamIdentity.Player)
        {
            animator.SetFloat("LastHorizontal", rb.velocity.x);
            animator.SetFloat("LastVertical", rb.velocity.y);
        }
        ZeroOutMovement();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        if (fixedtime >= duration)
        {
            animator.SetTrigger("RevertToIdle");
        }

    }
}
