using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDestructable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySkin;
    [SerializeField] private int timesToHit;
    private Material blankCanvas;
    private Material origColor;
    [SerializeField] private Color flash1 = Color.red;
    [SerializeField] private Color flash2 = Color.white;
    private bool dead = false;

    void Start()
    {
        mySkin = GetComponent<SpriteRenderer>();
        //Debug.Log(timesToHit);
    }
    private void TakeDamage()
    {
        Debug.Log("HitMe");
        StartCoroutine(Flash());
        timesToHit--;
       
        if(timesToHit<1&&!dead)
        {
            dead = true;
            Die();
        }
    }
    IEnumerator Flash()
    {
        float timer = 0;
        while(timer < 1.5f)
        {
            mySkin.color = flash1;
            yield return new WaitForSeconds(.05f);
            mySkin.color = flash2;
            yield return new WaitForSeconds(.05f);
            timer += Time.deltaTime + .2f;
            yield return null;
        }
        mySkin.color = Color.white; ;
    }
    private void Die()
    {
        Destroy(this.gameObject, .5f);
        //This is where you put a death animation and such 
        // or create particle effects. 
    }
}
