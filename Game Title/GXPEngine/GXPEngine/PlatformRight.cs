using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class PlatformRight : AnimationSprite
    {
        Timing destroyTimer;
        public PlatformRight(int xPos, int yPos) : base("spritesheet.png", 3, 1, -1, false, true)
        {
            SetFrame(2);

            x = xPos;
            y = yPos;

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
