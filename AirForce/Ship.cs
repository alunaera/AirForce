using System;

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
            PositionX -= 6;

            PositionX = PositionX + Size / 2 > MaxPositionX
                ? MaxPositionX - Size / 2
                : PositionX;
        }

        public void DestroyShip()
        {
            Health = 0;
        }

        protected int GetDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return (int)Math.Sqrt(componentX + componentY);
        }
    }
}
