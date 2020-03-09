using System;

namespace AirForce
{
    class Ship : ObjectOnGameField
    {
        // Магические значения - злое зло. Исправить.
        protected const int MaxPositionX = 1535;

        public void Move()
        {
            PositionX -= 6;

            PositionX = PositionX + Size / 2 > MaxPositionX
                ? MaxPositionX - Size / 2
                : PositionX;
        }

        public void DestroyShip()
        {
            Health = 0;
        }
    }
}
