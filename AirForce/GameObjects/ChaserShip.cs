using System;
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    internal class ChaserShip : ArmedGameObject
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

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();

            GameObject playerShipBullet =
                gameObjects.Where(gameObject => gameObject.ObjectType == ObjectType.PlayerBullet)
                           .OrderBy(gameObject => GetDistanceToObject(gameObject.PositionX, gameObject.PositionY))
                           .FirstOrDefault();

            if (playerShipBullet != null && Math.Abs(PositionY - playerShipBullet.PositionY) <= Size)
                PositionY += 3 * Math.Sign(PositionY - playerShipBullet.PositionY);

            PositionX -= 8;
        }
    }
}
