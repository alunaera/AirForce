using System;
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    internal class ChaserShip : Ship
    {
        public ChaserShip(int positionX, int positionY)
        {
            ObjectType = ObjectType.ChaserShip;
            Bitmap = Properties.Resources.ChaserShip;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 80;
        }

        public override void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();

            var playerShipBullets =
                gameObjects.Where(gameObject => gameObject.ObjectType == ObjectType.PlayerBullet)
                           .OrderBy(gameObject => GetDistanceToObject(gameObject.PositionX, gameObject.PositionY));

            if (playerShipBullets.Any() && Math.Abs(PositionY - playerShipBullets.First().PositionY) <= Size)
                PositionY += Math.Sign(PositionY - playerShipBullets.First().PositionY) * 3;

            PositionX -= 8;
        }
    }
}
