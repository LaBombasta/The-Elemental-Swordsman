using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBoxType : MonoBehaviour
{
    public enum HitType 
    {
        Directional = 0,
        Centerpoint = 1,
        Perpendicular =2,
        Centerpull =3,
    }
    //DEBUG Animator is taking care of knockback. Switch over to controlling with Character Stats
    public HitType hitType;

    public void ApplyKnockBack(GameObject hitObject, Vector2 inputDirection, float knockback)
    {
        //Debug.Log(hitObject);
        Rigidbody2D hitRb;
        Vector2 directionToTarget = (hitObject.transform.position - transform.position).normalized;
        if (hitObject.GetComponent<Rigidbody2D>())
        { 
            hitRb = hitObject.GetComponent<Rigidbody2D>();
            hitRb.velocity = Vector2.zero;
        }
        else
        {
            return;
        }
        switch (hitType)
        {
            case HitType.Directional:
                hitRb.AddForce(inputDirection * knockback, ForceMode2D.Impulse);
                break;
            case HitType.Centerpoint:
                hitRb.AddForce(directionToTarget* knockback, ForceMode2D.Impulse);
                break;
            case HitType.Perpendicular:
                Vector2 perp = Vector2.Perpendicular((inputDirection).normalized);
                float dotProduct = Vector2.Dot(directionToTarget, perp);
                if(dotProduct>=0)
                {
                    hitRb.AddForce(perp * knockback, ForceMode2D.Impulse);
                } else
                {
                    hitRb.AddForce(-perp * knockback, ForceMode2D.Impulse);
                }
                break;
            case HitType.Centerpull:
                Vector2 perp2 = Vector2.Perpendicular((inputDirection).normalized);
                float dotProduct2 = Vector2.Dot(directionToTarget, perp2);
                if (dotProduct2 >= 0)
                {
                    hitRb.AddForce(-perp2 * knockback, ForceMode2D.Impulse);
                }
                else
                {
                    hitRb.AddForce(perp2 * knockback, ForceMode2D.Impulse);
                }
                break;
            default:
                break;
        }
    }
}
