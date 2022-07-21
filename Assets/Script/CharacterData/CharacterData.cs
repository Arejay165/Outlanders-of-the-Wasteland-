using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterStats", fileName = "New CharacterStats")]
public class CharacterData : CharacterBaseData
{
    public float minHealthPoints;
    public float healthPoints;
    public float moveSpeed;
    public float minMoveSpeed;
    public float jumpForce;
    public float minJumpForce;
    public SoundEvent sound;
   
}
