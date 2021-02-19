using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class SceneManager : GameObject
    {
        MyGame myGame;

        public AnimationSprite scene1;
        public AnimationSprite scene2;
        public AnimationSprite scene3;

        private Timing scene1Timing;
        private Timing scene2Timing;
        private Timing scene3Timing;

        public SceneManager()
        {
            myGame = (MyGame)game;

            scene1 = new AnimationSprite("Scene1.png", 5, 1, -1, false, false);
            scene2 = new AnimationSprite("Scene2.png", 2, 1, -1, false, false);
            scene3 = new AnimationSprite("Scene3.png", 2, 1, -1, false, false);

            scene1Timing = new Timing();
            scene2Timing = new Timing();
            scene3Timing = new Timing();

            scene1.alpha = 0;
            scene2.alpha = 0;
            scene3.alpha = 0;

            AddChild(scene1);
            AddChild(scene2);
            AddChild(scene3);

            scaleX = 0.80f;
            scaleY = 0.74f;
        }

        void Update()
        {
            if(myGame.player.lives == 0)
            {
                if(myGame.level.maxReachedLevel == 1)
                {
                    scene1.alpha = 1;
                    scene2.alpha = 0;
                    scene3.alpha = 0;
                    if(scene1Timing.Repeat(100, false))
                    {
                        scene1Timing.repeated = false;
                        scene1.NextFrame();
                    }
                }
                if (myGame.level.maxReachedLevel == 2)
                {
                    scene1.alpha = 0;
                    scene2.alpha = 1;
                    scene3.alpha = 0;
                    if (scene2Timing.Repeat(500, false))
                    {
                        scene2Timing.repeated = false;
                        scene2.NextFrame();
                    }
                }
                if (myGame.level.maxReachedLevel == 3)
                {
                    scene1.alpha = 0;
                    scene2.alpha = 0;
                    scene3.alpha = 1;
                    if (scene3Timing.Repeat(500, false))
                    {
                        scene3Timing.repeated = false;
                        scene3.NextFrame();
                    }
                }
            }
        }
    }
}
