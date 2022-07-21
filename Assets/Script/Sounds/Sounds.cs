using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Sounds : ScriptableObject
{
    // Start is called before the first frame update

    public abstract void PlayAudio(AudioSource source, int audioIndex);
}
