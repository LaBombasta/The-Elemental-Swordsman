using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWood : BasicSpell
{
    public void Implosion()
    {
        AudioSource.PlayClipAtPoint(SFXCexplosion, transform.position, 1f);

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
                    Vector2 distance = j.transform.position - transform.position;
                    float explosionForce = explosionForceMultiplier / distance.magnitude;
                    j.BroadcastMessage("TakeDamage", _AttackValues, SendMessageOptions.DontRequireReceiver);
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(distance.normalized * -explosionForce);
                }
            }

        }
    }
    
}
