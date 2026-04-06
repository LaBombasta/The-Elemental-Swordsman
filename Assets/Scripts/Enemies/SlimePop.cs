using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePop : MonoBehaviour
{
    [SerializeField] private GameObject subeffect;
    [SerializeField] private bool popMe;
    [SerializeField] float scaling;
    [SerializeField] Vector3 originalScale;
    private bool myEventTrigger;
    // Start is called before the first frame update

    private void Start()
    {
        originalScale = transform.localScale;
        myEventTrigger = true;
    }
    private void Update()
    {
        transform.localScale = new Vector3(originalScale.x*scaling, originalScale.x * scaling, 1f);
        //Debug.Log(transform.localScale+" "+ originalScale+" "+scaling);
    }
    public void SpawnSubEffect()
    {
        StartCoroutine(Spawn());
    }
    public IEnumerator Spawn()
    {
        if (myEventTrigger)
        {
            Instantiate(subeffect, transform.position, transform.rotation);
            myEventTrigger = false;
        }
        yield return new WaitForEndOfFrame();
        myEventTrigger = true;

    }

    public void PopMe()
    {
        if(popMe)
        {
            Destroy(this.gameObject);
        }
        
    }
}
