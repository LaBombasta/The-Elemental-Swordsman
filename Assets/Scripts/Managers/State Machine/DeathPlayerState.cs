using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayerState : HurtState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        ZeroOutMovement();
        duration = 5f;
        animator.SetTrigger("Death");
        rb.isKinematic = true;
        //AudioManager.instance.PlaySoundEffects(AudioManager.instance.Dying);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (fixedtime >= duration)
        {
            Application.LoadLevel(Application.loadedLevel);
            //stateMachine.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();

    }
}

