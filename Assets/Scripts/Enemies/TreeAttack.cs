using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAttack : MonoBehaviour
{
    private GameObject target;
    [SerializeField]private GameObject delayedAttack;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void targetedAttack()
    {
        Instantiate(delayedAttack, target.transform.position, transform.rotation);
    }
}
