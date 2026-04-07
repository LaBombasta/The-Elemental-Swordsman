using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeEnemies;
    [SerializeField] private GameObject[] areaClearItems;
    private bool triggered = false;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CheckNullList()&&!triggered)
        {
            if (collision.gameObject.GetComponent<CharacterStats>().team == TeamIdentity.Player)
            {
                Debug.Log("AREA CLEAR WOO");
                UnhideItems();
                triggered = true;
                AudioManager.instance.PlayVictorySFX();
                anim.SetTrigger("Death");
            }
        }
    }
    private bool CheckNullList()
    {
        activeEnemies.RemoveAll(x => x == null);
        if (activeEnemies.Count == 0)
        {
            //Area Clear!!
            //AudioManager.instance.PlayBreakingShield();
            anim.SetTrigger("Death");
            return true;
            
        }
        return false;
    }
    private void UnhideItems()
    {
        for (int i = 0; i< areaClearItems.Length; i++)
        {
            areaClearItems[i].SetActive(true);
        }
    }
}
