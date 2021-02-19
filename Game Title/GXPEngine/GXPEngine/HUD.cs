using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GXPEngine
{
    public class HUD : Canvas
    {
        MyGame myGame;

        Sprite overlay;

        Font main1Font = new Font("VCR OSD Mono", 22);
        Font main2Font = new Font("VCR OSD Mono", 85);
        Font main3Font = new Font("VCR OSD Mono", 45);

        bool canLoop = true;
        bool looping;
        bool isDelayingLoop = false;

        public float goblinPos;

        bool enableInsertCoinText = false;

        Timing blinkInsertCoinTextOn;
        Timing blinkInsertCoinTextOff;

        public HUD() : base(1520, 790, false)
        {
            myGame = (MyGame)game;
            overlay = new Sprite("overlay.png", false, false);
            //AddChild(overlay);
            overlay.alpha = 0.6f;

            blinkInsertCoinTextOn = new Timing();
            blinkInsertCoinTextOff = new Timing();
        }

        void Update()
        {
            graphics.Clear(Color.Empty);           
            if (myGame.player.lives != 0)
            {
                x = -myGame.x;
            }
            if (myGame.isPlaying && myGame.gameOver == false)
            {
                graphics.DrawString("Score: " + (int)Math.Round(myGame.score, 0), main1Font, Brushes.White, 0, 0);          
                graphics.DrawString("Lives: " + myGame.player.lives, main1Font, Brushes.White, 650, 0);          
                graphics.DrawString("Highscore: 9848", main1Font, Brushes.White, 1245, 0);
            }
            if(!myGame.isPlaying)
            {
                graphics.DrawString("[SPACE] to jump, [SHIFT] to attack", main1Font, Brushes.White, 450, 440);
                if (enableInsertCoinText)
                {
                    graphics.DrawString("PRESS [ENTER] TO PLAY", main2Font, Brushes.White, 20, 300);
                }
            }
            if (myGame.gameOver)
            {
                Console.WriteLine(myGame.gameOver);
                graphics.DrawString("GAME OVER", main2Font, Brushes.White, 400, 0);
                graphics.DrawString("Press [ENTER] to continue", main3Font, Brushes.White, 300, 720);

                if(Input.GetKey(Key.ENTER))
                {
                    myGame.GameReset();
                }
            }
            if (canLoop)
            {
                looping = true;
                isDelayingLoop = true;
            }

            if (isDelayingLoop)
            {
                if (blinkInsertCoinTextOff.Delay(1000))
                {
                    canLoop = true;
                    isDelayingLoop = false;
                    blinkInsertCoinTextOff.canDelay = true;
                    enableInsertCoinText = false;
                }
            }

            if (looping)
            {
                canLoop = false;
                if (blinkInsertCoinTextOn.Delay(500))
                {
                    looping = false;
                    blinkInsertCoinTextOn.canDelay = true;
                    enableInsertCoinText = true;
                }
            }
        }
    }
}
