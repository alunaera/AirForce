﻿namespace AirForce
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

        public bool IsIntersectionWithPlayerShip(PlayerShip playerShip)
        {
            return GetDistanceToObject(playerShip.PositionX, playerShip.PositionY) <= (Size + playerShip.Size) / 2;
        }

        public void TakeDamage<T>()
        {
            switch (typeof(T).ToString())
            {
                case "AirForce.PlayerShip":
                    DestroyShip();
                    break;
                case "AirForce.Ground":
                case "AirForce.Meteor":
                    DestroyShip();
                    break;
            }
        }
    }
}
