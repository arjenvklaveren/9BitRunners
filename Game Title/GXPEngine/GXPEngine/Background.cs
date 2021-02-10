using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Background : Sprite
    {
        Timing destroyTimer;
        public Background(int posX, int posY) : base("backGround.png", false, false)
        {
            x = posX;
            y = posY;

            destroyTimer = new Timing();
        }

        void Update()
        {
            DestroyItSelf();
        }

        void DestroyItSelf()
        {
            if (destroyTimer.Delay(10000))
            {
                Destroy();
            }
        }
    }
}
