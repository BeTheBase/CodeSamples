using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Babylonian
{
    public class PlayerMove : PlayerBase
    {
        public float Speed = 50f;
        public float RunMultiplier = 2f;
        public bool Running;

        bool right = false;
        bool left = false;
        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 input = value.ReadValue<Vector2>();

            if (input.x > 0)
            {
                SpumAnimator.PlayAnimation(1);

                right = true;
                left = false;
            }
            else if (input.x < 0)
            {
                SpumAnimator.PlayAnimation(1);

                left = true;
                right = false;
            }
            else
            {
                SpumAnimator.PlayAnimation(0);
                left = false;
                right = false;
            }
        }


        void FixedUpdate()
        {
            Running = false;

            //var right = InputState.GetButtonValue(InputButtons[0]);
            //var left = InputState.GetButtonValue(InputButtons[1]);
            var run = InputState.GetButtonValue(InputButtons[2]);

            if (right || left)
            {
                //AManager.PlayPlayerSound("PlayerWalk");
                var tmpSpeed = Speed;

                if (run && RunMultiplier > 0)
                {
                    tmpSpeed *= RunMultiplier;
                    Running = true;
                }

                var velX = tmpSpeed * (float)InputState.direction;
                
                //velocity movement:
                Body2D.velocity = new Vector2(velX, Body2D.velocity.y);
                //rigidbody movetowards:
                //Body2D.MovePosition(new Vector2(velX, Body2D.velocity.y));
            }

        }
    }
}
