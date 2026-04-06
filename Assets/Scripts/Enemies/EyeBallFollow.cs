using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallFollow : MonoBehaviour
{
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotateDirection = (target.transform.position - transform.position).normalized;
        
        float angle= Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

    }
}
