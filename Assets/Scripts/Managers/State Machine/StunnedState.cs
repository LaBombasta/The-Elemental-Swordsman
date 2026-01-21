using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : HurtState
{
    //EXPAND THIS INTO "HURT" State
    //Use is as the basis for Knockdown and Death
    // Start is called before the first frame update
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator.SetTrigger("Stunned");
        duration = characterStats.GetStunTime();
        //AudioManager.instance.PlaySoundEffects(AudioManager.instance.EnemyHit);
        if(characterStats.team == TeamIdentity.Player)
        {
            animator.SetFloat("LastHorizontal", rb.velocity.x);
            animator.SetFloat("LastVertical", rb.velocity.y);
        }
        
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
