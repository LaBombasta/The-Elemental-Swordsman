using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWood : BasicSpell
{
    private Animator ani;
    public override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        ani.SetInteger("TeamFlag", (int)teamFlag);

    }

    public void Implosion()
    {
        
        AudioSource.PlayClipAtPoint(SFX_explosion, transform.position, 1f);

        if (VFX_explosion != null)
        {
            Destroy(Instantiate(VFX_explosion, transform.position, Quaternion.identity), 3);
        }
        collidersToDamage = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D j in collidersToDamage)
        {
            CharacterStats hitEntity = j.GetComponentInChildren<CharacterStats>();


            // Only check colliders with a valid Team Componnent attached
            if (hitEntity && (hitEntity.team != teamFlag))
            {
                Rigidbody2D rb = j.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float explosionForce;
                    Vector2 distance = j.transform.position - transform.position;
                    if(distance.magnitude !=0)
                    {
                        explosionForce = explosionForceMultiplier / distance.magnitude;
                    }
                    else
                    {
                        explosionForce = explosionForceMultiplier / .01f;
                    }
                    
                    j.BroadcastMessage("TakeDamage", AttackValues, SendMessageOptions.DontRequireReceiver);
                    rb.velocity = new Vector2(0, 0);
                    if(distance.normalized != Vector2.zero)
                    {
                        rb.AddForce(distance.normalized * -explosionForce);
                    }
                    else
                    {
                        rb.AddForce(new Vector2(.01f, .01f) * -explosionForce);
                    }
                   
                }
            }

        }
    }
    
}
