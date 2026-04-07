using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterStats>())
        {
            CharacterStats theCharacter = collision.gameObject.GetComponent<CharacterStats>();
            if (theCharacter.team == TeamIdentity.Player)
            {
                AudioManager.instance.PlayHeal();
                theCharacter.HealMe(healAmount);
                this.gameObject.SetActive(false);
            }
        }
        

    }
}
