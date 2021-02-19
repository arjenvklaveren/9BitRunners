using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GXPEngine
{
    public class Background : GameObject
    {
        MyGame myGame;

        public int backGroundLeftPosX;
        public int backGroundRightPosX;

        public Sprite backgroundLevel1SpriteLeft;
        public Sprite backgroundLevel1SpriteRight;

        public Sprite backgroundLevel2SpriteLeft;
        public Sprite backgroundLevel2SpriteRight;

        public Sprite backgroundLevel3SpriteLeft;
        public Sprite backgroundLevel3SpriteRight;

        private bool hasMovedBackGroundLeft;
        private bool hasMovedBackGroundRight;

        public float previousBackgroundLeftResetPos = 1920;
        public float previousBackgroundRightResetPos = 3840;

        public Background(int posX, int posY)
        {
            x = posX;
            y = posY;
            
            scaleX = 1f;
            scaleY = 0.74f;

            myGame = (MyGame)game;

            backgroundLevel1SpriteLeft = new Sprite("background4.png", false, false);
            backgroundLevel1SpriteRight = new Sprite("background4.png", false, false);

            backgroundLevel2SpriteLeft = new Sprite("background5.png", false, false);
            backgroundLevel2SpriteRight = new Sprite("background5.png", false, false);

            backgroundLevel3SpriteLeft = new Sprite("background6.png", false, false);
            backgroundLevel3SpriteRight = new Sprite("background6.png", false, false);

            backGroundLeftPosX = 0;
            backGroundRightPosX = 1920;

            backgroundLevel1SpriteLeft.alpha = 1;
            backgroundLevel1SpriteRight.alpha = 1;
            backgroundLevel2SpriteLeft.alpha = 0;
            backgroundLevel2SpriteRight.alpha = 0;
            backgroundLevel3SpriteLeft.alpha = 0;
            backgroundLevel3SpriteRight.alpha = 0;

            AddChild(backgroundLevel1SpriteLeft);
            AddChild(backgroundLevel1SpriteRight);
            AddChild(backgroundLevel2SpriteLeft);
            AddChild(backgroundLevel2SpriteRight);
            AddChild(backgroundLevel3SpriteLeft);
            AddChild(backgroundLevel3SpriteRight);
        }

        void Update()
        {
            SetBackGroundPositions();
            LoopBackGround();
            TransitionToNextBackGround();
        }

        void LoopBackGround()
        {
            x += 8;
            previousBackgroundLeftResetPos += 8;
            previousBackgroundRightResetPos += 8;

            if (myGame.player.x > previousBackgroundLeftResetPos + 400 && hasMovedBackGroundLeft == false)
            {
                previousBackgroundLeftResetPos += 3840;
                hasMovedBackGroundLeft = true;
                backGroundLeftPosX += 3840;
                hasMovedBackGroundLeft = false;
            }
            if (myGame.player.x > previousBackgroundRightResetPos + 400 && hasMovedBackGroundRight == false)
            {
                previousBackgroundRightResetPos += 3840;
                hasMovedBackGroundRight = true;
                backGroundRightPosX += 3840;
                hasMovedBackGroundRight = false;
            }
        }

        void TransitionToNextBackGround()
        {
            //Console.WriteLine(myGame.level.changeBackgroundPlatformPos);
            if(myGame.player.x > myGame.level.changeBackgroundPlatformPos)
            {
                if (myGame.level.maxReachedLevel < myGame.level.currentLevel)
                {
                    myGame.level.maxReachedLevel = myGame.level.currentLevel;
                }
                if (myGame.level.currentLevel == 2)
                {
                    if(backgroundLevel1SpriteLeft.alpha > 0.1f)
                    {
                        backgroundLevel1SpriteLeft.alpha -= 0.05f;
                        backgroundLevel1SpriteRight.alpha -= 0.05f;
                    }
                    if(backgroundLevel2SpriteLeft.alpha < 1)
                    {
                        backgroundLevel2SpriteLeft.alpha += 0.05f;
                        backgroundLevel2SpriteRight.alpha += 0.05f;
                    }
                }
                if (myGame.level.currentLevel == 3)
                {

                    if (backgroundLevel2SpriteLeft.alpha > 0.1f)
                    {
                        backgroundLevel2SpriteLeft.alpha -= 0.05f;
                        backgroundLevel2SpriteRight.alpha -= 0.05f;
                    }
                    if (backgroundLevel3SpriteLeft.alpha < 1)
                    {
                        backgroundLevel3SpriteLeft.alpha += 0.05f;
                        backgroundLevel3SpriteRight.alpha += 0.05f;
                    }
                }
                
            }            
        }

        void SetBackGroundPositions()
        {
            backgroundLevel1SpriteLeft.x = backGroundLeftPosX;
            backgroundLevel1SpriteRight.x = backGroundRightPosX;
            backgroundLevel2SpriteLeft.x = backGroundLeftPosX; 
            backgroundLevel2SpriteRight.x = backGroundRightPosX;
            backgroundLevel3SpriteLeft.x = backGroundLeftPosX; 
            backgroundLevel3SpriteRight.x = backGroundRightPosX;
        }
    }
}
