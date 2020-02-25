using System;
using System.Drawing;

namespace AirForce
{
    class Ship : IMovable
    {
        public int PositionX;
        public int PositionY;
        public int Health;

        public Ship()
        {
            Health = 1;
        }

        public void Move()
        {

        }
    }
}
