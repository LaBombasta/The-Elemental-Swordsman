using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEffectSpawn : MonoBehaviour
{
    [SerializeField] private GameObject subeffect;
    [SerializeField] private float spawnInterval;
    private float timer =.1f;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;    
        }
        else
        {
            Instantiate(subeffect, transform.position, transform.rotation);
            timer = spawnInterval;
        }
        
    }
}
