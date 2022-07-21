using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CutsceneCanvas : MenuCanvas
{
    // Start is called before the first frame update
    public Cutscene cutscene;
    public Image cutsceneImage;
    public Text cutsceneText;
    public int cutsceneIndex;

    public GameObject textDisplay;
    public TextMeshPro dialogueText;
    public float typingSpeed;

    protected override void Start()
    {
        base.Start();
        
    }

    
    public void AddIndex()
    {
        cutsceneIndex++;
        NextDialogue();
    }

    public void NextDialogue()
    {
        if(cutscene.dialogues.Length != cutsceneIndex)
        {
            cutsceneText.text = cutscene.dialogues[cutsceneIndex];
            StartCoroutine(TypeSentence(cutsceneText.text));
            cutsceneImage.sprite = cutscene.imageCutscenes[cutsceneIndex];
        }
        else
        {
            //Give Flexibility
            GameMngr.Instance.LoadLevel("Intro", "Level1");
            GameMngr.Instance.StartGame();
        }
       

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

}
