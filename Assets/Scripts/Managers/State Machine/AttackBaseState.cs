using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBaseState : State
{
    protected enum AttackType 
    {
        Light,
        Heavy,
        NONE,
    }

    protected int attackIndex;
    protected float duration;
    protected float speed;
    protected float[] damageValues = new float[6];
    protected float knockback;
    protected Vector2 movement;

    protected bool canComboL;
    protected bool canComboH;
    protected bool canComboN;
    protected bool pressed = false;
    protected bool attackTransition = false;
    private bool nudgeTrigger = false;

    protected Rigidbody2D rb;
    protected Collider2D hitCollider;
    protected HitBoxType hitBehaviour;
    protected MagicSystem magicSystem;
    private List<GameObject> collidersDamaged;
    
    protected Animator animator;
    protected AttackType attackType;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitBehaviour = GetComponent<CharacterStats>().hitType;
        magicSystem = GetComponent<MagicSystem>();

        collidersDamaged = new List<GameObject>();
        hitCollider = characterStats.hitbox;
        

        if(characterStats.team == TeamIdentity.Player)
        {
            movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            if (movement != Vector2.zero)
            {
                animator.SetFloat("LastHorizontal", movement.x);
                animator.SetFloat("LastVertical", movement.y);
            }
        }
    }

    public override void OnExit()
    {
        pressed = false;
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (!pressed)
        {
            InputManager._lightAttack.performed += context => canComboL = true;
            InputManager._heavyAttack.performed += context => canComboH = true;
            InputManager._magicPrep.performed += context => canComboN = true;
            pressed = true;
        }


        if (animator.GetFloat("ActiveAttack") > 0f)
        {
            Attack();
        }
        

    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Nudge(speed);
    }
    protected void ZeroOutMovement()
    {
        rb.velocity = Vector2.zero;
    }
    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i].gameObject))
            {
                CharacterStats hitEntity = collidersToDamage[i].GetComponentInChildren<CharacterStats>();

                // Only check colliders with a valid Team Componnent attached
                if (hitEntity && (hitEntity.team != characterStats.team))
                {
                    collidersToDamage[i].BroadcastMessage("TakeDamage",damageValues, SendMessageOptions.DontRequireReceiver);
                    //GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform);
                    //Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
                    collidersDamaged.Add(collidersToDamage[i].gameObject);
                    hitBehaviour.ApplyKnockBack(collidersToDamage[i].gameObject, movement, knockback);
                }
            }
        }
    }
    
    public virtual void Nudge(float stepSpeed)
    {
        if(animator.GetFloat("ActiveStep")> 0f)
        {
            if (!nudgeTrigger)
            {
                movement.Set(InputManager.Movement.x, InputManager.Movement.y);
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);

                if (movement != Vector2.zero)
                {
                    animator.SetFloat("LastHorizontal", movement.x);
                    animator.SetFloat("LastVertical", movement.y);
                }

                nudgeTrigger = true;
            }
            movement = new Vector2(animator.GetFloat("LastHorizontal"), animator.GetFloat("LastVertical"));
            rb.AddForce(movement * stepSpeed, ForceMode2D.Force);
            //rb.velocity = movement * stepSpeed;
        }
        else
        {
            ZeroOutMovement();
        }
    }

    public virtual void AssignAttackValues(int index)
    {
        AttackEditorValues attackValue = characterStats.AttackValue[index];
        damageValues[0] = attackValue.baseDamage;
        damageValues[1] = attackValue.magicDamage;
        damageValues[2] = (int)attackValue.magicType;
        if(GetComponent<MagicSystem>())
        {damageValues[2] = (float)GetComponent<MagicSystem>().GetCurrentMagicType();}
        damageValues[3] = (int)attackValue.statusEffect;
        damageValues[4] = attackValue.StunTime;
        
        duration = attackValue.Duration;
        speed = attackValue.Speed;
        
        knockback = attackValue.Knockback;
    }
    

}
