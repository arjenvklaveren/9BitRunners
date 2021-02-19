using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Player : Sprite
    {
        MyGame myGame;

        public int lives = 3;

        public float playerSpeed;

        public AnimationSprite playerAnimation;

        private Timing animationTimer;
        private Timing moveTimer;

        public float animationSpeed = 5;

        int animationStartFrame;
        int animationDuration;

        bool canRepeatAnimation = true;

        private bool canJump;

        private float velocity;

        private float previousY;
        public float previousX;

        public bool attacking;
        private bool isDelayingAttack;
        public bool canAttack = true;

        public Sprite playerHitBox;

        public Sprite attackHitBox;
        Timing attackTime;
        Timing attackDelay;

        public Sound jumpSound;
        public Sound swordSwingSound;
        public Sound swordHitSound;

        public Player() : base("hitboxPlayer.png", false, true)
        {
            myGame = (MyGame)game;

            playerAnimation = new AnimationSprite("all4.png", 4, 4, -1, false, false);

            attackHitBox = new Sprite("hitboxAttack.png", false, false);
            
            animationTimer = new Timing();
            moveTimer = new Timing();
            attackTime = new Timing();
            attackDelay = new Timing();

            playerAnimation.y -= 45;
            playerAnimation.x -= 15;

            attackHitBox.x += 90;
            attackHitBox.y = y - 50;
            attackHitBox.height += 80;

            x = 400;
            y = 500;

            playerSpeed = 12;

            velocity = 1;

            jumpSound = new Sound("jump.wav", false, false);
            swordSwingSound = new Sound("swordSwing.wav", false, false);
            swordHitSound = new Sound("swordHit.wav", false, false);

            AddChild(playerAnimation);
        }

        void Update()
        {
            if(lives != 0)
            {
                Move();
            }
            Jump();
            Attack();

            AnimatePlayer();

            if(animationSpeed > 0)
            {
                //animationSpeed -= 0.02f;
            }

            myGame.scrollSpeed = playerSpeed;
            
            if(y > 1500)
            {
                lives--;
                myGame.Reset();
            }
            if (x > previousX + myGame.level.spawnPlatformSpeed)
            {
                previousX = x;
                myGame.level.BuildPlatform();
            }
        }

        void AnimatePlayer()
        {
            playerAnimation.SetCycle(animationStartFrame, animationDuration, animationSpeed, true, canRepeatAnimation);
            if (animationTimer.Repeat(10, false))
            {
                animationTimer.repeated = false;
                playerAnimation.Animate();
            }        
        }

        void Move()
        {
            if(moveTimer.Repeat(100, false))
            {
                moveTimer.repeated = false;
                playerSpeed += 0.005f;
                animationSpeed -= 0.005f;
            }
            MoveUntilCollision(playerSpeed, 0);
        }
        
        void Jump()
        {
            if (previousY != y)
            {
                animationStartFrame = 11;
                animationDuration = 2;              
                previousY = y;
                canJump = false;
            }
            velocity = velocity + 0.9f;
            //velocity = velocity + 0.5f; 
            MoveUntilCollision(0, velocity);
            if(previousY == y)
            {
                canJump = true;
                velocity = 0;
                if (!attacking)
                {
                    animationStartFrame = 0;
                    animationDuration = 8;
                    canRepeatAnimation = true;
                }
                if(Input.GetKeyDown(Key.SPACE) && canJump)
                {
                    velocity = -20;
                    canRepeatAnimation = false;
                    jumpSound.Play();
                }
            }
        }

        void Attack()
        {
            if(Input.GetKey(Key.LEFT_SHIFT) && canAttack)
            {
                swordSwingSound.Play();
                attacking = true;
                animationSpeed = 2; 
                isDelayingAttack = true;
                canRepeatAnimation = false;
                if(!canJump)
                {
                    velocity = -10;
                }
            }

            if (isDelayingAttack)
            {
                if (attackDelay.Delay(750))
                {
                    canAttack = true;
                    isDelayingAttack = false;
                    attackDelay.canDelay = true;
                }
            }

            if(attacking)
            {
                animationStartFrame = 8;
                animationDuration = 3;

                canAttack = false;
                AddChild(attackHitBox);           
                if (attackTime.Delay(500))
                {
                    attacking = false;
                    animationSpeed = 5;
                    RemoveChild(attackHitBox);
                    attackTime.canDelay = true;
                }
            }
        }
    }
}
