using System;
using System.Drawing;

namespace AirForce
{
    class Ship : IMovable
    {
        // Магические значения - злое зло. Исправить.
        protected const int MaxPositionX = 1535;

        public int Size { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Health { get; protected set; }

        public void Move()
        {
            PositionX -= 8;

            PositionX = PositionX + Size / 2 > MaxPositionX
                ? MaxPositionX - Size / 2
                : PositionX;

            PositionX = PositionX - Size / 2 < 0
                ? Size / 2
                : PositionX;

            PositionY = PositionY - Size / 2 < 0
                ? Size / 2
                : PositionY;
        }
    }
}
