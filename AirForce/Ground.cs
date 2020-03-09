using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    class Ground : ObjectOnGameField
    {
        public Ground()
        {
            ObjectType = ObjectType.Ground;
            PositionX = 0;
            PositionY = 750;
        }
    }
}
