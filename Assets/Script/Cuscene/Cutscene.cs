using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Cutscene", fileName = "New Cutscene")]
public class Cutscene : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogues;
    public Sprite[] imageCutscenes;
    public int[] directionTextBox;
    public AudioClip[] soundType;
}
