using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeEnemies;

    private bool triggered = false;
    public bool finalTrigger = false;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStats>())
        {
            CheckNullList();
            if (collision.gameObject.GetComponent<CharacterStats>().team == TeamIdentity.Player && !triggered)
            {
                triggered = true;
                for(int i = 0; i< activeEnemies.Count; i++)
                {
                    activeEnemies[i].GetComponent<EnemyAi>().PursuitTrigger();
                }
            }
            
        }
    }
    private void CheckNullList()
    {
        activeEnemies.RemoveAll(x => x == null);
        if (activeEnemies.Count == 0 && !finalTrigger)
        {
            finalTrigger = true;
            //AudioManager.instance.PlayBreakingShield();
            //anim.SetTrigger("Death");
        }
    }
}
