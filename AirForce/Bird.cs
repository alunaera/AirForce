using System;

namespace AirForce
{
    internal class Bird : IMovable
    {
        public int PositionX;
        public int PositionY;
        public int Health;
        public int Size;

        public Bird(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 30;
        }

        public void Move()
        {
            PositionX -= 10;
        }

        public void TakeDamage<T>()
        {
            if (typeof(T).ToString() == "AirForce.PlayerShip")
                DestroyBird();
        }

        public void DestroyBird()
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
