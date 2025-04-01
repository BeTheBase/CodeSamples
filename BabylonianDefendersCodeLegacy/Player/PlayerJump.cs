using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Babylonian
{
    public class PlayerJump : PlayerBase
    {
        public float JumpSpeed = 200f;
        public float JumpDelay = .1f;
        public int JumpCount = 2;

        protected float lastJumpTime = 0;
        protected int jumpsRemaining = 0;

        bool canJump = false;
        public void OnJumpInput(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                canJump = true;
            }
            if(value.canceled)
            {
                canJump = false;
            }
        }

        void FixedUpdate()
        {
            //var canJump = InputState.GetButtonValue(InputButtons[0]);
            var holdTime = InputState.GetButtonHoldTime(InputButtons[0]);

            if (CollisionState.standing)
            {
                if (canJump && holdTime < .1f)
                {
                    jumpsRemaining = JumpCount - 1;
                    OnJump();
                }
            }
            else
            {
                if (canJump && holdTime < .1f && Time.time - lastJumpTime > JumpDelay)
                {
                    if (jumpsRemaining > 0)
                    {
                        OnJump();
                        jumpsRemaining--;
                    }
                }
            }
        }

        public virtual void OnJump()
        {
            var vel = Body2D.velocity;
            lastJumpTime = Time.time;
            Body2D.velocity = new Vector2(vel.x, JumpSpeed);
        }
    }
}