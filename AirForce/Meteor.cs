using System;

namespace AirForce
{
    internal class Meteor : IMovable
    {
        public int PositionX;
        public int PositionY;
        public int Size { get; }
        public int Health;

        public Meteor(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            Size = 160;
            Health = 5;
        }

        public void Move()
        {
            PositionX -= 10;
            PositionY += 4;
        }

        public void Destroy()
        {
            Health = 0;
        }

        public bool IsIntersection(int positionX, int positionY, int size)
        {
            return GetDistanceToObject(positionX, positionY) <= (Size + size) / 2;
        }

        private int GetDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return (int)Math.Sqrt(componentX + componentY);
        }
    }
}
