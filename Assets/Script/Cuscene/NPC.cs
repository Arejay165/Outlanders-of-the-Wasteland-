using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public Cutscene cutscene;
    public int cutsceneIndex;
    private Animator anim;
    private bool hasEnter;

    public GameObject textDisplay;
    public TextMeshPro dialogueText;
    public float typingSpeed;
    public AudioSource typeSound;

    private void Start()
    {
        anim = GetComponent<Animator>();

        textDisplay.SetActive(false);
        typingSpeed = 0.03f;

        typeSound = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && hasEnter)
        {

            //   StartCoroutine(StartConvoDialogue());
            StartConvoDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            typeSound.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IngameCanvas.instances.tutorialText.text = "Press [V] to interact";
            hasEnter = true;
            anim.SetBool("HasEnter", true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            textDisplay.SetActive(false);
            IngameCanvas.instances.cutsceneText.text = "";
            IngameCanvas.instances.tutorialText.text = "";
            cutsceneIndex = 0;
            anim.SetTrigger("HasExit");
            anim.SetBool("HasEnter", false);
            anim.SetBool("IsTalking", false);
            hasEnter = false;
            typeSound.Play();
        }
           
    }




    public void StartConvoDialogue()
    {
        anim.SetBool("IsTalking", true);
        textDisplay.SetActive(true);

        if (cutscene.dialogues.Length != cutsceneIndex)
        {

            // IngameCanvas.instances.cutsceneText.text = cutscene.dialogues[cutsceneIndex];

            StopAllCoroutines();
            StartCoroutine(TypeSentence(cutscene.dialogues[cutsceneIndex]));

            cutsceneIndex++;
        }
        else
        {
            cutsceneIndex = 0;
            // IngameCanvas.instances.cutsceneText.text = cutscene.dialogues[0];
            //  IngameCanvas.instances.cutsceneText.text = cutscene.dialogues[cutsceneIndex];

            StopAllCoroutines();
            StartCoroutine(TypeSentence(cutscene.dialogues[cutsceneIndex]));
        }


        Debug.Log(cutsceneIndex);
    }

    public IEnumerator TypeSentence(string sentence)
    {
        typeSound.Play();
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        typeSound.Stop();
    }

}
