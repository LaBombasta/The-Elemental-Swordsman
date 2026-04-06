using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRootSpawn : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject ObjectToSpawn;
    public float timer = .01f;
    void Start()
    {
        StartCoroutine(Creation());
    }

    public IEnumerator Creation()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Instantiate(ObjectToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
            yield return new WaitForSeconds(timer);
        }
    }

  
}
