using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenBoss : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BossHealth;
    
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<CharacterStats>() && !triggered)
        {
            TeamIdentity PlayerOnlyTrigger = collision.GetComponent<CharacterStats>().team;
            if (PlayerOnlyTrigger == TeamIdentity.Player)
            {
                StartCoroutine(StartBossMusic());
                Boss.SetActive(true);
                BossHealth.SetActive(true);
                triggered = true;
            }
        }
    }
    private IEnumerator StartBossMusic()
    {
        yield return new WaitForSeconds(2);
        AudioManager.instance.PlayBossMusic();
    }
}
