using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Enemy : AnimationSprite
    {
        MyGame myGame;

        Timing idleAnimationTimer;

        public Enemy(float xPos, float yPos) : base("goblinSheet.png", 6, 1, -1, false, false)
        {
            SetFrame(0);

            x = xPos;
            y = yPos;

            scale = 0.7f;

            y += 35;

            idleAnimationTimer = new Timing();

            myGame = (MyGame)game;
        }

        void Update()
        {
            IdleAnimation();
            DetectIfHitPlayer();
            DetectIfHit();
        }


        void IdleAnimation()
        {
            if (idleAnimationTimer.Repeat(100, false))
            {
                idleAnimationTimer.repeated = false;
                NextFrame();
            }
        }

        void DetectIfHitPlayer()
        {
            if(myGame.player.x + myGame.player.width > x && myGame.player.x < x + width && myGame.player.y + myGame.player.height >= y && myGame.player.y < y + height)
            {
                myGame.Reset();
                myGame.player.lives--;
            }
        }

        void DetectIfHit()
        {
            if(myGame.player.attacking)
            {
                if(myGame.player.x + myGame.player.attackHitBox.x + myGame.player.attackHitBox.width > x && myGame.player.x + myGame.player.attackHitBox.x < x && myGame.player.y - myGame.player.attackHitBox.y + myGame.player.attackHitBox.height - 50 > y && myGame.player.y - myGame.player.attackHitBox.y < y)
                {
                    Destroy();
                    myGame.player.swordHitSound.Play();
                }
            }    
        }
    }
}
