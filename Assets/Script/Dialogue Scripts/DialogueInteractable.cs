using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    public Dialogue dialogue;

    bool canInteract;
    public bool dialogueStarted;

    private void Start()
    {
        canInteract = false;
    }

    private void Update()
    {
      
        if (canInteract == true && Input.GetKeyDown(KeyCode.Q))
        {
            if (dialogueStarted == false)
            {
                Debug.Log("Interacting");
                TriggerDialogue();
                dialogueStarted = true;
            }
            else
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Capsule can be interacted with");
        if (collision.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Player is within Trigger area");
        if (collision.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Capsule cannot be interacted with");
        canInteract = false;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
