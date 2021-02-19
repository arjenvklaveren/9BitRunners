using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GXPEngine
{
    public class Level : GameObject
    {
        MyGame myGame;

        public Background background;

        public int currentLevel = 1;

        public int maxReachedLevel = 1;

        public float currentPlatformPosX = 0;
        public float currentPlatformPosY = 700;

        int platformSize = 128;
        public float distanceBetweenPlatforms = 200;
        public int platformSpriteOffset = 0;
        private int platformMaxWidth = 4;

        public int startPlatformAmount = 999;

        public int previousPlatformAmount;

        private int levelPlatformSize = 100;

        public int backgroundPosX;

        public float spawnPlatformSpeed = 500;

        public float changeBackgroundPlatformPos = 99999999;

        public int decideIfEnemyNumber1;
        public int decideIfEnemyNumber2;
        public int decideIfEnemyNumber3;

        public bool hasBuildBeginningPlatform;

        public ArrayList platforms = new ArrayList();
        public ArrayList enemies = new ArrayList();

        public Level()
        {
            myGame = (MyGame)game;
            background = new Background(0, 0);
            AddChild(background);
        }

        void Update()
        {
            GenerateLevel();
            distanceBetweenPlatforms += 0.05f;
        }

        void GenerateLevel()
        {
            //Made starting platform
            if(hasBuildBeginningPlatform == false)
            {
                for (int i = 0; i < startPlatformAmount; i++)
                {
                    Platform pfMiddle = new Platform(currentPlatformPosX, currentPlatformPosY, 1 + platformSpriteOffset);
                    currentPlatformPosX += platformSize;
                    platforms.Add(pfMiddle);
                    AddChild(pfMiddle);
                }            
                Platform pfRight = new Platform(currentPlatformPosX, currentPlatformPosY, 2 + platformSpriteOffset);
                currentPlatformPosX += distanceBetweenPlatforms * 2;
                platforms.Add(pfRight);
                AddChild(pfRight);

                hasBuildBeginningPlatform = true;
            }
        }

        public void BuildPlatform()
        {
            var random = new Random();

            int decideIfEnemyOnPlatform = random.Next(0, 5);

            //Generate Left part of platform
            Platform pfLeft = new Platform(currentPlatformPosX, currentPlatformPosY, 0 + platformSpriteOffset);
            currentPlatformPosX += platformSize;
            platforms.Add(pfLeft);
            AddChild(pfLeft);

            //Generate middle parts of platform
            int platformLength = random.Next(0, platformMaxWidth);
            int decideWhichPlatformEnemyCouldSpawn = random.Next(0, platformLength);
         
            for(int i = 0; i < platformLength; i++)
            {
                Platform pfMiddle = new Platform(currentPlatformPosX, currentPlatformPosY, 1 + platformSpriteOffset);
                platforms.Add(pfMiddle);
                AddChild(pfMiddle);

                if (i == decideWhichPlatformEnemyCouldSpawn)
                {
                    if (decideIfEnemyOnPlatform == decideIfEnemyNumber1 || decideIfEnemyOnPlatform == decideIfEnemyNumber2 || decideIfEnemyOnPlatform == decideIfEnemyNumber3)
                    {
                        Enemy enemy = new Enemy(currentPlatformPosX, currentPlatformPosY - 110);
                        enemies.Add(enemy);
                        AddChild(enemy);
                    }
                }

                currentPlatformPosX += platformSize;
            }

            //Generate right part of platform
            Platform pfRight = new Platform(currentPlatformPosX, currentPlatformPosY, 2 + platformSpriteOffset);
            currentPlatformPosX += platformSize;
            platforms.Add(pfRight);
            AddChild(pfRight);

            //Decrease or Increase height of platform, or not
            int decideIfChangeHeight = random.Next(0, 2);

            if (decideIfChangeHeight == 1)
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
                    int decideIfChangeUpOrDown = random.Next(0, 2);
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
            SetLevelStats();
            currentPlatformPosX += distanceBetweenPlatforms;        
        }

        public void SetLevelStats()
        {
            if (platforms.Count > previousPlatformAmount + levelPlatformSize)
            {
                if (currentLevel < 3)
                {
                    changeBackgroundPlatformPos = currentPlatformPosX;
                    currentLevel++;
                }
                if (currentLevel == 1)
                {
                    decideIfEnemyNumber1 = 4;
                    decideIfEnemyNumber2 = 4;
                    decideIfEnemyNumber3 = 4;
                    platformMaxWidth = 5;
                }
                if (currentLevel == 2)
                {
                    decideIfEnemyNumber1 = 4;
                    decideIfEnemyNumber2 = 3;
                    decideIfEnemyNumber3 = 3;
                    platformMaxWidth = 4;
                }
                if (currentLevel == 3)
                {
                    decideIfEnemyNumber1 = 4;
                    decideIfEnemyNumber2 = 3;
                    decideIfEnemyNumber3 = 2;
                    platformMaxWidth = 3;
                }
                if (platformSpriteOffset < 6)
                {
                    platformSpriteOffset += 3;
                }
                previousPlatformAmount = platforms.Count;
            }
        }


        //Delete everything when you die
        public void DeletePlatforms()
        {
            foreach (GameObject platform in platforms)
            {
                platform.Destroy();
            }
            platforms.Clear();
        }

        public void DeleteEnemies()
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.Destroy();
            }
            enemies.Clear();
        }
    }
}
