using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public Text text;
    public string instructions;
    void Start()
    {
        text = GameMngr.Instance.instrucText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
            if(collision.gameObject.CompareTag("Player"))
            {

               text.text = instructions;
              }   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            text.text = instructions;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.text = "";
    }
}
