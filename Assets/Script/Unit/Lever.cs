using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Utilities
{
    // Start is called before the first frame update
    public GameObject disableLock;

    
    public override void OnHit()
    {
        base.OnHit();
        disableLock.SetActive(false);
    }
}
