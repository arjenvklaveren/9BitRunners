using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        MyGame myGame;

        private float playerSpeed;

        private Timing animationTimer;

        private bool canJump;

        private float velocity;

        private float previousY;

        public Player() : base("PlayerSpriteSheetFull.png", 4,4)
        {
            SetFrame(0);
            _frames = 8;

            myGame = (MyGame)game;

            animationTimer = new Timing();

            playerSpeed = 5;
            x = 500;
            velocity = 1;

            scale = 1f;
        }

        void Update()
        {
            Move();
            Jump();

            myGame.scrollSpeed = playerSpeed;
        }

        void Move()
        {
            if (animationTimer.Repeat(100, false))
            {
                animationTimer.repeated = false;
                NextFrame();
                //playerSpeed += 0.01f;
            }
            MoveUntilCollision(playerSpeed, 0);   
        }

        void Jump()
        {
            if (previousY != y)
            {
                previousY = y;
                canJump = true;
            }
            velocity = velocity + 0.2f; 
            MoveUntilCollision(0, velocity);
            if(previousY == y)
            {
                velocity = 0;
                if(Input.GetKeyDown(Key.SPACE) && canJump)
                {
                    velocity = -10;
                }
            }

        }
    }
}
