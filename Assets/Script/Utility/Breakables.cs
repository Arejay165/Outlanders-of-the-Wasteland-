using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : Utilities
{
    // Start is called before the first frame update

    public override void OnHit()
    {
        base.OnHit();
        Destroy(gameObject);
    }
}
