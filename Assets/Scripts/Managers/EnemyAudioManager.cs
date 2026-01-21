using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource SFXSource;

    public void PlaySoundEffects(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

