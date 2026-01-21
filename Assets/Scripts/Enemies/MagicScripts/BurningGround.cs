using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGround : BasicSpell
{
    [SerializeField] private float damageInterval;
    private float timer = 0;
    
    public override void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
        }else
        {
            dealDamage();
            timer = damageInterval;
        }
    }
    void dealDamage()
    {
        collidersToDamage = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D j in collidersToDamage)
        {
            CharacterStats hitEntity = j.GetComponentInChildren<CharacterStats>();
            // Only check colliders with a valid Team Componnent attached
            if (hitEntity && (hitEntity.team != teamFlag))
            {
                j.BroadcastMessage("TakeDamage", _AttackValues, SendMessageOptions.DontRequireReceiver);
            }
        }
        //collidersToDamage = null;
    }
}
