using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    // Start is called before the first frame update
    public SoundEvent sound;
    
    

    public virtual void OnHit()
    {
        Debug.Log(gameObject.name);
       sound.PlayAudio(gameObject.GetComponent<AudioSource>(), 0);
    }
}
