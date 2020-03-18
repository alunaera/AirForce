using System;
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    internal class BomberShip : Ship
    {
        public BomberShip(int positionX, int positionY)
        {
            ObjectType = ObjectType.BomberShip;
            Bitmap = Properties.Resources.BomberShip;
            PositionX = positionX;
            PositionY = positionY;
            DelayOfShot = 0;
            Health = 3;
            Size = 80;
        }

        public override void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            IncreaseDelayOfShot(30);

            createdObjects = new List<GameObject>();

            GameObject playerShip = gameObjects.First();

            if (Math.Abs(PositionY - playerShip.PositionY) <= playerShip.Size && DelayOfShot == 0)
            {
                createdObjects.Add(new BomberShipBullet(PositionX - Size, PositionY));
                SetDelayOfShotDefaultValue();
            }

            PositionX -= 6;
        }
    }
}
