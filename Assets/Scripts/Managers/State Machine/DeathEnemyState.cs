using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemyState : HurtState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        duration = 1.2f;
        animator.SetTrigger("Death");
        //GameManager.instance.UpdateScore(1);
        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (fixedtime >= duration)
        {
            Destroy(playerStats.thisCharacter);
            //stateMachine.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        
    }
}
