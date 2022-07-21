using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound Event")]
public class SoundEvent : Sounds
{
    public AudioClip[] clips;
    public override void PlayAudio(AudioSource source, int audioIndex)
    { 
        source.clip = clips[audioIndex];
        source.PlayOneShot(clips[audioIndex]);
    }
}
