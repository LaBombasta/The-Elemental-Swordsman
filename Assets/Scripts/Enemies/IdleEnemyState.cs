using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : State
{
    protected GameObject enemyCharacter;
    protected Animator animator;
    protected CharacterStats enemyStats;
    protected EnemyAi ai;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<CharacterStats>();
        ai = GetComponent<EnemyAi>();
        ai.EvaluateBehaviour();
        //ai.EvaluateBehaviour();
        //animator.SetFloat("Horizontal", 0);
        //animator.SetFloat("Vertical", 0);
        //FindAttackTarget();
        //Debug.Log("Entering Idle State");
        //InvokeRepeating()
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

    }
  
   
    public override void OnExit()
    {
        base.OnExit();
        ai.StopAllCoroutines();
    }
    protected void ZeroOutMovement()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }
        

}
