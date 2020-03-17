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

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList, out List<ObjectOnGameField> createdObjectsList)
        {
            IncreaseDelayOfShot(30);

            createdObjectsList = new List<ObjectOnGameField>();

            ObjectOnGameField playerShip = objectOnGameFieldsList.First();

            if (Math.Abs(PositionY - playerShip.PositionY) <= playerShip.Size && DelayOfShot == 0)
            {
                createdObjectsList.Add(new BomberShipBullet(PositionX - Size, PositionY));
                SetDelayOfShotDefaultValue();
            }

            PositionX -= 4;
        }
    }
}
