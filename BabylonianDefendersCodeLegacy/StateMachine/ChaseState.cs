using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babylonian
{
    public class ChaseState : StateMachineBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);


        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float moveSpeed = 5 * Time.deltaTime;

            animator.transform.position = Vector3.MoveTowards(animator.transform.position, PlayerPosition.position, moveSpeed);

            Vector3 playerDistance = PlayerPosition.position - animator.transform.position;

            if(playerDistance.sqrMagnitude < fireKnightController.AttackRadius1 )
            {
                //Player is within Attack1 range
                 animator.SetTrigger("Attack1");
                animator.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (playerDistance.sqrMagnitude < fireKnightController.AttackRadius2 )
            {
                //Player is within Attack2 range
                animator.SetTrigger("Attack2");
                animator.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}