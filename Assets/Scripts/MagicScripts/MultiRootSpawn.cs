using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRootSpawn : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject spell;
    void Start()
    {
        StartCoroutine(Creation());
    }

    public IEnumerator Creation()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Instantiate(spell, spawnPoint.transform.position, spawnPoint.transform.rotation);
            yield return new WaitForSeconds(.01f);
        }
    }

  
}
