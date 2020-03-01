using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    class Bird : IMovable
    {
        public int PositionX;
        public int PositionY;

        public Bird(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void Move()
        {

        }
    }
}
