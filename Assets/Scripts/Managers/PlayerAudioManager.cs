using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource SFXSource;
    [Header("--- Audio Clips ---")]
    public AudioClip SwordSwipe1;
    public AudioClip SwordSwipe2;
    public AudioClip power1;
    public AudioClip power2;
    public AudioClip power3;
    private bool canPlay = true;
    public void PlaySoundEffects(AudioClip clip)
    {
        if (canPlay)
        {
            SFXSource.PlayOneShot(clip);
            //Debug.Log(clip.name);
        }
        //StartCoroutine(nameof(EventDelay));
    }
    IEnumerator EventDelay() 
    {
        canPlay = false;
        yield return new WaitForSeconds(.05f);
        canPlay = true;
    }
    public void playpowerup1()
    {
        SFXSource.PlayOneShot(power1);
    }
    public void playpowerup2()
    {
        SFXSource.PlayOneShot(power2);
    }
    public void playpowerup3()
    {
        SFXSource.PlayOneShot(power3);
    }
    
}
