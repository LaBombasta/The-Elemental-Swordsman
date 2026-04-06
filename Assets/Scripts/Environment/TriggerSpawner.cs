using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSpawner : MonoBehaviour
{
    //UnityEvent m_enemyDeath = new UnityEvent();
    [SerializeField] private int WaveAmount;
    [SerializeField] private GameObject[] spawnPoints = new GameObject[3];
    [SerializeField] private List<GameObject> activeEnemies;
    [SerializeField] private GameObject[] enemyToSpawn;
    [SerializeField] private GameObject SpawnedWall;
    [SerializeField] public GameObject DestructWall;
    [SerializeField] private bool isWinner = false;
    [SerializeField] private AudioManager audio;
    private bool triggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterStats>())
        {
            if(collision.gameObject.GetComponent<CharacterStats>().team == TeamIdentity.Player&&!triggered)
            {
                triggered = true;
                StartCoroutine(CreateEnemies());
                
                if(SpawnedWall)
                {
                    SpawnedWall.SetActive(true);
                }
                
            }
            CheckNullList();
        }
        
    }
    public IEnumerator CreateEnemies()
    {
        //ClearNullList();
        for(int i = 0; i<WaveAmount; i++)
        {
            
            int rando = Random.Range((int)0, spawnPoints.Length);
            GameObject gameObject1 = Instantiate(enemyToSpawn[i], spawnPoints[rando].transform.position, Quaternion.identity);
            GameObject enemy = gameObject1;
            activeEnemies.Add(enemy);
            yield return new WaitForSeconds(.01f);
        }

    }

    private void CheckNullList()
    {
        activeEnemies.RemoveAll(x => x == null);
        if(activeEnemies.Count ==0)
        {
            if(DestructWall)
            {
                audio.PlayVictorySFX();
                Destroy(DestructWall);
            }

            Destroy(this.gameObject);
        }
    }
}
