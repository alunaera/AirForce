using System;
using System.Drawing;

namespace AirForce
{
    class PlayerShip : IMovable
    {
        public Point Position { get; private set; }

        public PlayerShip()
        {
            Position = new Point(50, 200);
        }

        public void Move()
        {

        }
    }
}
