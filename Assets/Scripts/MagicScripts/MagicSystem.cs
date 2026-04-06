using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MagicType
{
    Lightning,
    Fire,
    Water,
    Metal,
    None,
}
public class MagicSystem : MonoBehaviour
{
    /*This Classes goal is to manange the magic that is cast by our character.
     * 
     * Magic will be self contained, but will be cast from this script
     * 
     * */
    [SerializeField] private GameObject[] spellPrefab = new GameObject[16];
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float imbueTimer;

    [Header("Fire Spell")]
    [SerializeField] Image FireCooldownIndicator;
    [SerializeField] private float FireCoolDownTotal = 15;
    [SerializeField] private float FireCooldown = 0;

    [Header("Water Spell")]
    [SerializeField] Image WaterCooldownIndicator;
    [SerializeField] private float WaterCoolDownTotal = 15;
    [SerializeField] private float WaterCooldown = 0;

    [Header("Lightning Spell")]
    [SerializeField] Image LightningCooldownIndicator;
    [SerializeField] private float LightningCoolDownTotal = 15;
    [SerializeField] private float LightningCooldown = 0;

    [Header("Metal Spell")]
    [SerializeField] Image MetalCooldownIndicator;
    [SerializeField] private float MetalCoolDownTotal = 15;
    [SerializeField] private float MetalCooldown = 0;

    private string magicChoice = "";
    public MagicType currentMagicType = MagicType.None;
    [SerializeField] private ParticleSystem[] infusionParticles;

    //This will case the imbued element's spell for light or heavy
    //for this small part of the project it is oaky to just caset based on heay or light
    //to get proof of concept for this game design course
    private void Update()
    {
        if (FireCooldown > 0)
        {
            FireCooldown -= Time.deltaTime;
            FireCooldownIndicator.fillAmount = FireCooldown / FireCoolDownTotal;
        }

        if (WaterCooldown > 0)
        {
            WaterCooldown -= Time.deltaTime;
            WaterCooldownIndicator.fillAmount = WaterCooldown / WaterCoolDownTotal;
        }

        if (LightningCooldown > 0)
        {
            LightningCooldown -= Time.deltaTime;
            LightningCooldownIndicator.fillAmount = LightningCooldown / LightningCoolDownTotal;
        }

        if (MetalCooldown > 0)
        {
            MetalCooldown -= Time.deltaTime;
            MetalCooldownIndicator.fillAmount = MetalCooldown / MetalCoolDownTotal;
        }
    }
    public void CastMagic( int indexer)
    {
        /*This is going to index by 0 4 8 12 
         * 0 light attack infuse
         * 4 heavy attack infuse
         * 8 light attack Full Spell
         * 12 heavy attack Full Spell
         */
        if(currentMagicType != MagicType.None && spellPrefab[((int)currentMagicType + indexer)]!= null)
        { 
            Instantiate(spellPrefab[((int)currentMagicType + indexer)], spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        
    }
   
    public void MagicIndicatorTimers()
    {
        LightningCooldown = 0.01f;
        FireCooldown = 0.01f;
        WaterCooldown = 0.01f;
        MetalCooldown = 0.01f;

        //CastMagic(4);
        switch (currentMagicType)
        {
            case MagicType.Lightning:
                LightningCooldown = LightningCoolDownTotal;
                break;
            case MagicType.Fire:
                FireCooldown = FireCoolDownTotal;
                break;
            case MagicType.Water:
                WaterCooldown = WaterCoolDownTotal;
                break;
            case MagicType.Metal:
                MetalCooldown = MetalCoolDownTotal;
                break;
            default:
                Debug.Log("This is supposed to play a particle but you defaulted");
                break;
        }
    }

    public void FirstAttack(string attack)
    {
        magicChoice = string.Empty;
        magicChoice += attack;
        //Debug.Log(magicChoice);
        ParticlePulse();
    }
    public void SecondAttack(string attack)
    {
        if(magicChoice != string.Empty)
        {
            magicChoice += attack;
        }
        //Debug.Log(magicChoice);
        ParticlePulse();
    }

    public void ChooseMagic()
    {
        switch(magicChoice)
        {
            case "LightLight":
                currentMagicType = MagicType.Lightning;
                break;
            case "LightHeavy":
                currentMagicType = MagicType.Fire;
                break;
            case "HeavyLight":
                currentMagicType = MagicType.Water;
                break;
            case "HeavyHeavy":
                currentMagicType = MagicType.Metal;
                break;
            default:
                currentMagicType = MagicType.None;
                Debug.Log("Magic Choosing Default State, How in the world did you get here?");
                return;
        }
        //Debug.Log(activeMagic);
    }
    private IEnumerator ImbueSword(float timer)
    {
        imbueTimer = timer;
        MagicIndicatorTimers();
        while (imbueTimer>0)
        {
            
            imbueTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("I reset the magic");
        currentMagicType= MagicType.None;
    }
    
    //this will trigger a pulse of particle effects
    public void MagicPulse()
    {
        ChooseMagic();
        StopAllCoroutines();
        StartCoroutine(ImbueSword(15));
        ParticlePulse();
    }
    public void ParticlePulse()
    {
        switch (currentMagicType)
        {
            case MagicType.Lightning:
                //Debug.Log("Play particle lightning");
                infusionParticles[0].Play();
                break;
            case MagicType.Fire:
                //Debug.Log("Play particle Fire");
                infusionParticles[1].Play();
                break;
            case MagicType.Water:
                //Debug.Log("Play particle Water");
                infusionParticles[2].Play();
                break;
            case MagicType.Metal:
                //Debug.Log("Play particle Metal");
                infusionParticles[3].Play();
                break;
            default:
                //Debug.Log("There is no magic currently selected");
                break;
        }
    }
    public MagicType GetCurrentMagicType()
    {
        return currentMagicType;
    }
}
