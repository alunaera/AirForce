using System;

namespace AirForce
{
    internal class Bird : ObjectOnGameField
    {
        public Bird(int positionX, int positionY)
        {
            ObjectType = ObjectType.Bird;
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
    }
}
