using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    class EnemyShip : Ship
    {
        public EnemyShip(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 80;
        }
    }
}
