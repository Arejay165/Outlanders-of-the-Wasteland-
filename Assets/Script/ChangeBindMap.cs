using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeBindMap : MonoBehaviour
{
    // Start is called before the first frame update
 
    public GameObject blockables;
    public Boss boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
      
            Fade.instance.ActivateFade();
            blockables.SetActive(true);
            // boss.canFight = true;
            //  gameObject.SetActive(false);
        }
     

    }
}
