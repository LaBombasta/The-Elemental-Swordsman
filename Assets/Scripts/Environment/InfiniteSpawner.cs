using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InfiniteSpawner : MonoBehaviour
{
    //UnityEvent m_enemyDeath = new UnityEvent();
    [SerializeField] private int maxAmount;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private List<GameObject> activeEnemies;
    [SerializeField] private GameObject[] enemyToSpawn;

    private void Start()
    {
        InvokeRepeating("CreateEnemies", 1, spawnRate);
    }

    private void CreateEnemies()
    {
        ClearNullList();
        if (activeEnemies.Count<maxAmount)
        {
            int rando = Random.Range((int)0, spawnPoints.Length);
            int endo = Random.Range((int)0, enemyToSpawn.Length);
            GameObject gameObject1 = Instantiate(enemyToSpawn[endo], spawnPoints[rando].transform);
            GameObject enemy = gameObject1;
            activeEnemies.Add(enemy);
        }
        
    }

    private void ClearNullList()
    {
        activeEnemies.RemoveAll(x => x == null);
    }
}
