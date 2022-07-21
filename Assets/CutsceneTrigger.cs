using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneTrigger : MonoBehaviour
{
    protected Cutscene cutscene;
    public Cutscene cutsceneIntro;
    public Cutscene cutsceneOutro;
    public int cutsceneIndex;
    public bool hasEnter, hasStayed, dialogueStarted;

    public GameObject textDisplay;
    public Text dialogueText;
    public float typingSpeed;
    public AudioSource typeSound;

    public GameObject blockables;

    private Queue<string> dialogues;
    private int convoIndex;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hasEnter = false;
        hasStayed = false;

        textDisplay = IngameCanvas.instances.textBox;
        dialogueText = IngameCanvas.instances.cutsceneText;

        textDisplay.SetActive(false);
        typingSpeed = 0.03f;
        dialogues = new Queue<string>();
        convoIndex = 0;
        typeSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && hasEnter == true && dialogueStarted)
        {
       //     Debug.Log("Interacting");
            DisplayNextSentence();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasEnter == false && dialogueStarted == false && collision.gameObject.CompareTag("Player"))
        {
            GameMngr.Instance.player.canMove = false;
            GameMngr.Instance.boss.canFight = false;
            hasEnter = true;
            hasStayed = true;

            cutscene = cutsceneIntro;
            dialogueStarted = true;
            StartDialogue(cutscene);
            IngameCanvas.instances.tutorialText.text = "Press [v] to continue";
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (dialogueStarted == false && collision.gameObject.CompareTag("Player"))
        {
            if (GameMngr.Instance.defeatedCutscene == true || GameMngr.Instance.bossDefeatedCutscene == true)
            {
                cutscene = cutsceneOutro;
                dialogueStarted = true;
                GameMngr.Instance.player.canMove = false;
                GameMngr.Instance.player.canFlip = false;
                StartDialogue(cutscene);
                IngameCanvas.instances.tutorialText.text = "Press [v] to continue";
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        IngameCanvas.instances.tutorialText.text = "";
        hasStayed = false;
    }

    public void StartDialogue(Cutscene dialogue)
    {
        Time.timeScale = 0f;

        textDisplay.SetActive(true);
        // Debug.Log("Talking");
     
        dialogues.Clear();

        foreach (string sentence in dialogue.dialogues)
        {
            dialogues.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();

            return;
        }
        IngameCanvas.instances.SpriteDirectional(cutscene.directionTextBox[convoIndex]); // null image
        IngameCanvas.instances.dialogueBox[cutscene.directionTextBox[convoIndex]].sprite = cutscene.imageCutscenes[convoIndex]; // set image 
        convoIndex++;
        string sentence = dialogues.Dequeue();

     //   Debug.Log(sentence);

       

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        // dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        Time.timeScale = 1f;
        convoIndex = 0;
        Fade.instance.ActivateFade();

        IngameCanvas.instances.tutorialText.text = "";
        textDisplay.SetActive(false);
        IngameCanvas.instances.ResetDirectionalSprites();
        GameMngr.Instance.player.canMove = true;
        GameMngr.Instance.player.canFlip = true;

        if (GameMngr.Instance.boss.currentHP > 0)
        {
            GameMngr.Instance.boss.canFight = true;
        }

        if (GameMngr.Instance.currentLevel == "IntroCutscene")
        {
            GameMngr.Instance.player.hasLittleGirl = false;
        }

        Debug.Log("End of Dialogue");

        if (GameMngr.Instance.currentLevel == "IntroCutscene" && GameMngr.Instance.defeatedCutscene == true)
        {
            GameMngr.Instance.introLevel = false;
            GameMngr.Instance.defeatedCutscene = false;
            hasEnter = false;
            GameMngr.Instance.LoadLevel(GameMngr.Instance.currentLevel, "Level1");
            this.enabled = false;
        }
        else
        {
            blockables.SetActive(true);
            GameMngr.Instance.bossDefeatedCutscene = false;
        }

        dialogueStarted = false;
    }

    public IEnumerator TypeSentence(string sentence)
    {
    
        dialogueText.text = "";
        typeSound.clip = cutscene.soundType[convoIndex - 1];
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSecondsRealtime(typingSpeed);
            typeSound.Play();
         
        }
        
        typeSound.Stop();
    }

}
