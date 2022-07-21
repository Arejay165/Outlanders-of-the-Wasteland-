using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(menuName = "Perks", fileName = "New Perks")]
public abstract class Perks : ScriptableObject
{
    public float effectDuration;
    public float coolDownDuration;
    public float statsMultplier;
    public AudioClip obtainSfx, expireSfx, inEffectSfx;
    public int effectIndex;
    public Sprite icon;
    public abstract IEnumerator CooldownEffect(GameObject obj);
    public abstract void PlayAudio(AudioSource obj, AudioClip sfx);
    }

