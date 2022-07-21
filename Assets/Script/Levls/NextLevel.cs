using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public string currentLevel;
    public string nextLevel;
    public static NextLevel instance;
    public BoxCollider2D boxCollider2D;
    public SpriteRenderer sprite;
    void Start()
    {
        instance = this;
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        GameMngr.Instance.isKeyOn = false;
        boxCollider2D.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(GameMngr.Instance.isKeyOn);
        // Debug.Log(boxCollider2D.isTrigger);
    }

   
    public void Unlock()
    {
        // boxCollider2D.isTrigger = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        // sprite.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if (boxCollider2D == null)
        {
            Debug.Log("box collider not wordkingt");
        }

        if (sprite == null)
        {
            Debug.Log("sprite not wordkingt");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameMngr.Instance.isKeyOn == true)
        {
            // SceneManager.LoadScene(sceneName);
            GameMngr.Instance.LoadLevel(currentLevel, nextLevel);
        }
    }
}
