using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetChild(0).gameObject.SetActive(false);
        animator.transform.GetChild(1).gameObject.SetActive(false);


    }
}
