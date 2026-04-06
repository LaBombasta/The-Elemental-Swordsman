using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class Fireball : BasicSpell
{
    public float knockback;
    public override void Start()
    {
        base.Start();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            EndSpell();
            return;
        }
        collidersToDamage = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D j in collidersToDamage)
        {
            CharacterStats hitEntity = j.GetComponentInChildren<CharacterStats>();


            // Only check colliders with a valid Team Componnent attached
            if (hitEntity && (hitEntity.team != teamFlag))
            {
                j.BroadcastMessage("TakeDamage", AttackValues, SendMessageOptions.DontRequireReceiver);
                Rigidbody2D rb = j.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 distance = j.transform.position - transform.position;
                    float explosionForce = explosionForceMultiplier / distance.magnitude;
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(distance.normalized * explosionForce);
                }
            }

        }

    }
    public override void EndSpell()
    {
        Explode();
        if(lingeringEffect!=null)
        {
            Instantiate(lingeringEffect, transform.position, Quaternion.Euler(0,0,0));
        }
        Destroy(this.gameObject);
    }
}
