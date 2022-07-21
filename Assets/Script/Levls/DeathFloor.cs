using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFloor : Singleton<DeathFloor>
{
    // Start is called before the first frame update
    //public string currentLevel;
    public GameObject checkPointPosition;
    
    void Start()
    {
       // currentLevel = GameMngr.Instance.currentLevel;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //  GameMngr.Instance.RestartScene(currentLevel);
            if (GameMngr.Instance.player.currentHP > 0)
            {
                GameMngr.Instance.player.TakeDamage(1f);
                SpawnPos(collision.gameObject);
            }
            else
            {
                GameMngr.Instance.player.Death();
            }
        }


    }


    public void SpawnPos(GameObject obj)
    {
        obj.transform.position = checkPointPosition.transform.position;
    }
}
