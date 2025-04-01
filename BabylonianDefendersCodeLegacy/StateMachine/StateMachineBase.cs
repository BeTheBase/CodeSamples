using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Babylonian
{
    public class StateMachineBase : StateMachineBehaviour
    {
        public Transform PlayerPosition;

        public FireKnightController fireKnightController;

        public UnityEvent OnAttack1, OnAttack2;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            fireKnightController = animator.GetComponent<FireKnightController>();
            PlayerPosition = fireKnightController.PlayerReference;

        }
    }
}
