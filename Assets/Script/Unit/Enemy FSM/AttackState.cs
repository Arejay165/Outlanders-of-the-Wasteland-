using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseEnemyFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        if (Time.time >= NPC.GetComponent<Enemy>().nextAttackTime)
        {
            NPC.GetComponent<Enemy>().Attack();
            NPC.GetComponent<Enemy>().nextAttackTime = Time.time + 1f / NPC.GetComponent<Enemy>().attackRate;
        }
        */
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
