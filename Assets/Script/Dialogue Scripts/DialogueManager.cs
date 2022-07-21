using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;

    public Animator animator;

    public TextMeshPro nameText;
    public TextMeshPro dialogueText;
    public float typingSpeed;

    private Queue<string> dialogueSentences;

    void Start()
    {
        dialogueUI.SetActive(false);
        typingSpeed = 0.03f;
        dialogueSentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0f;

        CutsceneIn();

        dialogueUI.SetActive(true);
        Debug.Log("Talking with " + dialogue.name);
        nameText.text = dialogue.name;

        dialogueSentences.Clear();

        foreach (string sentence in dialogue.dialogueSentences)
        {
            dialogueSentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueSentences.Dequeue();

        Debug.Log(sentence);
     
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        // dialogueText.text = sentence;
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        Time.timeScale = 1f;

        CutsceneOut();

        Debug.Log("End of Dialogue");
        dialogueUI.SetActive(false);
        FindObjectOfType<DialogueInteractable>().dialogueStarted = false;
    }

    public void CutsceneIn()
    {
        animator.SetTrigger("CutsceneIn");
    }

    public void CutsceneOut()
    {
        animator.SetTrigger("CutsceneOut");
        animator.SetTrigger("ReturnToDefaultState");
    }
}
