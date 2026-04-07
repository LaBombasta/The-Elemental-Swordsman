using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{
    [Header("Movement Controls")]
    public float nextWaypointDistance = 3;
    public Seeker seeker;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;
    public float patrolDistance;
    public Transform target;

    [SerializeField] private bool pursue = false;
    private bool LockedOn;
    
    

    [Header("State Machine")]
    public StateMachine stateMachine;
    private Animator animator;
    private CharacterStats enemyCharacter;
    private enum MoveDirection{Up, Down, Left, Right, None};

    //These next few Variable are to make use of the A* plugin
    
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    
    Rigidbody2D rb;


    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        //InvokeRepeating("EvaluateBehaviour", 0, 1);
        if(!pursue)
        {
            StartCoroutine(Patrol());
        }else
        {
            StartCoroutine(Pursue());
        }
        
        //seeker.StartPath(rb.position, target.position, OnPathComplete);
        //StartEnemyBehaviour();
    }
    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        enemyCharacter = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
    }

    public void EvaluateBehaviour()
    {
        //Debug.Log("Choosing Action");
        if(stateMachine.CurrentState.GetType() == typeof(IdleEnemyState))
        {
            if (DetectionPulse(detectionRadius))
            {
                LockedOn = true;
                StopAllCoroutines();
                StartCoroutine(Pursue());
            }
            else if (!DetectionPulse(detectionRadius * 3))
            {
                LockedOn = false;
                StopAllCoroutines();
                StartCoroutine(Patrol());
            }else if(LockedOn)
            {
                StartCoroutine(Pursue());
            }
        
        }
        
    }
    public void PursuitTrigger()
    {
        StartCoroutine(Pursue());
    }
    public void EvaluateAttack()
    {
        if (stateMachine.CurrentState.GetType() == typeof(IdleEnemyState))
        {
            if(DetectionPulse(attackRadius))
            {
                rb.velocity = Vector2.zero;
                
                StopAllCoroutines();
                stateMachine.SetNextState(new AttackEnemyState());
            }
        }
    }
    public IEnumerator Pursue()
    { 
        for(; ; )
        {
            UpdatePath();
            float randomInterval = Random.Range(.8f, 1.3f);
            yield return new WaitForSeconds(randomInterval);
            //AttackEvaluation;
            EvaluateAttack();
            yield return new WaitForSeconds(randomInterval);
            EvaluateBehaviour();
        }
    }
    public IEnumerator Patrol()
    {
        //Debug.Log("patrol");
        MoveDirection lastDirection = MoveDirection.None;
        yield return new WaitForSeconds(1f);
        for (; ; )
        {
            EvaluateBehaviour();
            MoveDirection patrolDir = (MoveDirection)Random.Range(0, 4);
            while(patrolDir == lastDirection)
            {
                patrolDir = (MoveDirection)Random.Range(0, 4);
            }
            lastDirection = patrolDir;

            int patrolRange = (int)Random.Range(1, (patrolDistance + 1));
            switch (patrolDir)
            {
                case MoveDirection.Up:
                    UpdatePath(rb.position + (Vector2.up * patrolRange));
                    break;
                case MoveDirection.Down:
                    UpdatePath(rb.position + (Vector2.down * patrolRange));
                    break;
                case MoveDirection.Left:
                    UpdatePath(rb.position + (Vector2.left * patrolRange));
                    break;
                case MoveDirection.Right:
                    UpdatePath(rb.position + (Vector2.right * patrolRange));
                    break;
                default:
                    break;
            }
            
            yield return new WaitForSeconds(Random.Range(2,3.5f));  
        }
    }
    
    protected bool DetectionPulse(float rad)
    {
  
        Collider2D[] CollidersToHit = Physics2D.OverlapCircleAll(transform.position, rad);
        foreach (Collider2D j in CollidersToHit)
        {
            CharacterStats hitEntity = j.GetComponentInChildren<CharacterStats>();

            // Only check colliders with a valid Team Componnent attached
            if (hitEntity && (hitEntity.team == TeamIdentity.Player))
            {
                target = hitEntity.gameObject.transform;
                return true;
            }
        }
        return false;
    }
    protected void MoveInDirection(Vector2 dir)
    {
        UpdatePath(rb.position + dir);
    }
    protected void StopAllMovement()
    {
        rb.velocity = Vector2.zero;
    }
    
    protected void Dodge()
    {
        Vector2 directionToTarget = ((Vector2)target.position - rb.position).normalized;
        Vector2 perp = Vector2.Perpendicular(directionToTarget);
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            perp = -perp;
        }
        UpdatePath(rb.position + perp);
    }
    
    void UpdatePath() //Vector3 newPosition
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, (target.position + new Vector3((float)Random.Range(-3f, 3f), (float)Random.Range(-3f, 3f), 0)), OnPathComplete);
        }
        
    }
    void UpdatePath(Vector3 newPosition)
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, newPosition, OnPathComplete);
        }
    }
    
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void LookAtTarget()
    {
        Vector2 facingDirection = ((Vector2)target.position - rb.position).normalized;

        //Debug.Log(facingDirection);
        animator.SetFloat("Horizontal", facingDirection.x);
        animator.SetFloat("Vertical", facingDirection.y);
        animator.SetFloat("LastHorizontal", facingDirection.x);
        animator.SetFloat("LastVertical", facingDirection.y);
        /*
        if (Mathf.Abs(facingDirection.y) >= Mathf.Abs(facingDirection.x))
        {
            float sign = facingDirection.y / Mathf.Abs(facingDirection.y);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", sign);
            animator.SetFloat("LastHorizontal", 0f);
            animator.SetFloat("LastVertical", sign);
        }
        else
        {
            float sign = facingDirection.x / Mathf.Abs(facingDirection.x);
            animator.SetFloat("Horizontal", sign);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("LastHorizontal", sign);
            animator.SetFloat("LastVertical", 0f);
        }
        */


    }
    void LookAtVelocity()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
        animator.SetFloat("LastHorizontal", rb.velocity.x);
        animator.SetFloat("LastVertical", rb.velocity.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            //Debug.Log(path.vectorPath.Count);
            //Debug.Log(currentWaypoint);
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        if (stateMachine.CurrentState.GetType() == typeof(IdleEnemyState))
        {

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * enemyCharacter.MoveSpeed * Time.deltaTime;
            rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance<nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if(LockedOn && stateMachine.CurrentState.GetType() == typeof(IdleEnemyState))
        {
            LookAtTarget();
        }else if(stateMachine.CurrentState.GetType() == typeof(IdleEnemyState))
        {
           LookAtVelocity();
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
    }

}
