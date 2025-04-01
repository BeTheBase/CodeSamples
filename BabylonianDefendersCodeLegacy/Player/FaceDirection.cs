using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Babylonian
{
    public class FaceDirection : PlayerBase
    {
        bool right = false;
        bool left = false;
        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 input = value.ReadValue<Vector2>();

            if (input.x > 0)
            {
                right = true;
                left = false;
            }
            else if (input.x < 0)
            {
                left = true;
                right = false;
            }


        }

        // Update is called once per frame
        void FixedUpdate()
        {
           // var right = InputState.GetButtonValue(InputButtons[0]);
            //var left = InputState.GetButtonValue(InputButtons[1]);

            if (right)
            {
                InputState.direction = Directions.Right;
            }
            else if (left)
            {
                InputState.direction = Directions.Left;
            }

            transform.localScale = new Vector3((float)InputState.direction, transform.localScale.y, transform.localScale.z);
        }
    }
}
