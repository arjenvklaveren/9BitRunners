using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Platform : AnimationSprite
    {
        MyGame myGame;
        public Platform(float xPos, float yPos, int frame) : base("platformSpriteSheet.png", 9, 1, -1, false, true)
        {
            SetFrame(frame);

            x = xPos;
            y = yPos;

            myGame = (MyGame)game;
        }

        void Update()
        {

        }
    }
}
