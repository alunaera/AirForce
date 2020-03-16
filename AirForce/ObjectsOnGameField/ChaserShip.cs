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

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList)
        {
            var playerShipBulletList =
                objectOnGameFieldsList.Where(objectOnField => objectOnField.ObjectType == ObjectType.PlayerBullet)
                    .OrderBy(objectOnField => GetDistanceToObject(objectOnField.PositionX, objectOnField.PositionY));

            if (playerShipBulletList.Any() && Math.Abs(PositionY - playerShipBulletList.First().PositionY) <= Size)
                PositionY += Math.Sign(PositionY - playerShipBulletList.First().PositionY) * 5;

            PositionX -= 6;
        }
    }
}
