using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonState : State
{
    protected Animator animator;
    protected float duration = 2.3f;
    protected BossEnemyAi bossAI;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        bossAI = GetComponent<BossEnemyAi>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Summon");
        //Debug.Log("Entering Summon state");
    }
    public override void OnUpdate()
    {

        base.OnUpdate();
        if(fixedtime >= duration/2)
        {
            animator.ResetTrigger("Summon");
        }
        if (fixedtime>=duration)
        {
            //Debug.Log(fixedtime);
            
            stateMachine.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        
        
        //Debug.Log("Exiting Summon State");
    }

}
