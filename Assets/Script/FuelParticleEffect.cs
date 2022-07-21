using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelParticleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
