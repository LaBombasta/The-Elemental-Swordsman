using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBossState : HurtState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        duration = 2.05f;
        animator.SetTrigger("Death");
        GameManager.instance.UpdateScore(1);
        //Debug.Log("I'm dyin boss");
        //AudioManager.instance.PlaySoundEffects(AudioManager.instance.Dying);
    }
    public override void OnUpdate()
    {

        base.OnUpdate();
        
        if (fixedtime >= duration)
        {
            GameManager.instance.EndGame();
            Destroy(playerStats.thisCharacter);
            //stateMachine.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        
    }
}
