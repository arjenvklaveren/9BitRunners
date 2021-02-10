using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Level : GameObject
    {
        int currentPlatformPosX = 500;
        int currentPlatformPosY = 700;

        int platformAmount = 10;

        int backgroundPosX;

        Timing generatePlatformTimer;

        public Level()
        {
            generatePlatformTimer = new Timing();
        }

        void Update()
        {
            GenerateLevel();
        }

        void GenerateLevel()
        {
            if (generatePlatformTimer.Repeat(500, false))
            {
                generatePlatformTimer.repeated = false;
                BuildPlatform();
            }
        }

        void BuildPlatform()
        {
            if (platformAmount >= 5)
            {
                Console.WriteLine("ddd");
                BuildBackground();
                platformAmount = 0;
            }

            //Generate Left part of platform
            PlatformLeft pfLeft = new PlatformLeft(currentPlatformPosX, currentPlatformPosY);
            currentPlatformPosX += 128;
            platformAmount++;
            AddChild(pfLeft);

            //Generate middle parts of platform
            var rand1 = new Random();
            int platformLength = rand1.Next(0, 8);
            
            for(int i = 0; i < platformLength; i++)
            {
                PlatformMiddle pfMiddle = new PlatformMiddle(currentPlatformPosX, currentPlatformPosY);
                currentPlatformPosX += 128;
                platformAmount++;
                AddChild(pfMiddle);
            }
            
            //Generate right part of platform
            PlatformRight pfRight = new PlatformRight(currentPlatformPosX, currentPlatformPosY);
            currentPlatformPosX += 128;
            platformAmount++;
            AddChild(pfRight);

            //Decrease or Increase height of platform, or not
            var rand2 = new Random();
            int decideIfChangeHeight = rand2.Next(0, 2);

            if(decideIfChangeHeight == 1)
            {
                if(currentPlatformPosY == 700)
                {
                    currentPlatformPosY -= 150;
                }
                else if(currentPlatformPosY == 250)
                {
                    currentPlatformPosY += 150;
                }
                else
                {
                    var rand3 = new Random();
                    int decideIfChangeUpOrDown = rand3.Next(0, 2);
                    if(decideIfChangeUpOrDown == 1)
                    {
                        currentPlatformPosY -= 150;
                    }
                    else
                    {
                        currentPlatformPosY += 150;
                    }
                }
            }
            currentPlatformPosX += 128;
        }

        void BuildBackground()
        {         
            Background backGround = new Background(backgroundPosX, 0);
            backgroundPosX += 1900;

            AddChild(backGround);
        }
    }
}
