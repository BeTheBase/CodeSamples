using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babylonian
{
    public class PlayerWallJump : PlayerBase
    {
        public Vector2 JumpVelocity = new Vector2(50, 200);
        public bool JumpingOffWall;
        public float ResetDelay = .2f;

        private float timeElapsed = 0;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (CollisionState.onWall && !CollisionState.standing)
            {
                var canJump = InputState.GetButtonValue(InputButtons[0]);

                if (canJump && !JumpingOffWall)
                {
                    InputState.direction = InputState.direction == Directions.Right ? Directions.Left : Directions.Right;
                    Body2D.velocity = new Vector2(JumpVelocity.x * (float)InputState.direction, JumpVelocity.y);

                    ToggleScripts(false);
                    JumpingOffWall = true;
                }
            }
            
            if (JumpingOffWall)
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed > ResetDelay)
                {
                    ToggleScripts(true);
                    JumpingOffWall = false;
                    timeElapsed = 0;
                }
            }
        }
    }
}
