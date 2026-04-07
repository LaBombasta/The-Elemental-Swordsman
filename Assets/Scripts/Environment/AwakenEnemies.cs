using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeEnemies;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStats>())
        {
            if (collision.gameObject.GetComponent<CharacterStats>().team == TeamIdentity.Player && !triggered)
            {
                triggered = true;
                for (int i = 0; i < activeEnemies.Count; i++)
                {
                    activeEnemies[i].SetActive(true);
                }
            }

        }
    }
}
