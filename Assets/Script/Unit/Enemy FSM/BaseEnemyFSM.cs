using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;

    public float speed;

    public virtual void Awake()
    {
        speed = 2.0f;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<Enemy>().GetPlayer();
    }
}