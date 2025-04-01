using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateMachineBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerDistance = PlayerPos.position - animator.transform.position;
        if (playerDistance.z < animator.transform.position.z -3)
        {
            animator.SetBool("Attacking", true);
            return;
        }
     
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

}
