using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource MusicSource;
    public float musicVolume;
    [SerializeField] AudioClip Victory;
    [SerializeField] AudioClip BreakingShield;
    [SerializeField] AudioClip crackingShield;
    [SerializeField] AudioClip bossRoar;
    [SerializeField] AudioClip bossHurt;
    [SerializeField] AudioClip bossDeath;
    [SerializeField] AudioClip bossMusic;
    [SerializeField] AudioClip bossWarp;
    [SerializeField] AudioClip bossWarpReversed;
    [SerializeField] AudioClip bossPowerUp;
    [SerializeField] AudioClip bossSpellFired;
    [SerializeField] AudioClip wallCrumble;
    [SerializeField] AudioClip wallImmune;
    [SerializeField] AudioClip heal;



    public void Start()
    {
        instance = this;
    }


    public void PlaySoundEffects(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayVictorySFX()
    {
        SFXSource.PlayOneShot(Victory);
    }
    public void PlayBreakingShield()
    {
        SFXSource.PlayOneShot(BreakingShield);
    }
    public void PlayCrackingShield()
    {
        SFXSource.PlayOneShot(crackingShield);
    }
    public void PlayBossRoar()
    {
        SFXSource.PlayOneShot(bossRoar);
    }
    public void PlayBossHurt()
    {
        SFXSource.PlayOneShot(bossHurt);
    }
    public void PlayBossDeath()
    {
        SFXSource.PlayOneShot(bossDeath);
    }

    public void PlayWarp()
    {
        SFXSource.PlayOneShot(bossWarp);
    }

    public void PlayWarpReversed()
    {
        SFXSource.PlayOneShot(bossWarpReversed);
    }
    public void PlayBossPowerUp()
    {
        SFXSource.PlayOneShot(bossPowerUp);
    }
    public void PlayBossSpellFired()
    {
        SFXSource.PlayOneShot(bossSpellFired);
    }
    public void PlayWallCrumble()
    {
        SFXSource.PlayOneShot(wallCrumble);
    }
    public void PlayWallImmune()
    {
        SFXSource.PlayOneShot(wallImmune);
    }
    public void PlayHeal()
    {
        SFXSource.PlayOneShot(heal);
    }
    public void PlayBossMusic()
    {
        //MusicSource.volume = musicVolume;
        MusicSource.Play();
    }

}
