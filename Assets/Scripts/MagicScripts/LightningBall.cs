using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : BasicSpell
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            EndSpell();
            return;
        }
        if (collision.gameObject.GetComponent<CharacterStats>())
        {
            if(collision.gameObject.GetComponent<CharacterStats>().team != teamFlag)
            {
                EndSpell();
            }
            if(collision.gameObject.GetComponent<EnvironmentDestructable>())
            {
                collision.BroadcastMessage("TakeDamage", AttackValues,SendMessageOptions.DontRequireReceiver);
            }
            
        }
    }
    public override void EndSpell()
    {
        Explode();
        if (lingeringEffect != null)
        {
            Instantiate(lingeringEffect, transform.position, Quaternion.Euler(0, 0, 0));
        }
        Destroy(this.gameObject);
    }
}
