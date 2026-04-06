using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDestructable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySkin;
    [SerializeField] private int timesToHit;
    [SerializeField] private MagicType weakness; 
    [SerializeField] private Sprite[] imageSwaps;
    private int spriteIndex = 0;
    [SerializeField] private Color flash1 = Color.red;
    [SerializeField] private Color flash2 = Color.white;
    private Color origColor;
    private bool dead = false;

    void Start()
    {
        origColor = GetComponent<SpriteRenderer>().color;
        //Debug.Log(timesToHit);
    }

    public void TakeDamage(float[] damageValues)
    {
        
        if((MagicType)damageValues[2] != weakness||dead == true)
        {
            AudioManager.instance.PlayWallImmune();
            return; 
        }
        AudioManager.instance.PlayWallCrumble();
        //Debug.Log("HitMe");
        StartCoroutine(Flash());
        timesToHit--;
        spriteIndex++;
        mySkin.sprite = imageSwaps[spriteIndex];
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
        mySkin.color = origColor;
    }
    private void Die()
    {
        Destroy(this.gameObject, .1f);
        //This is where you put a death animation and such 
        // or create particle effects. 
    }
}
