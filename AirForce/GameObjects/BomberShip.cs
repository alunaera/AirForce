using System;
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    internal class BomberShip : ArmedGameObject
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

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            DecreaseDelayOfShot(3);

            createdObjects = new List<GameObject>();

            GameObject playerShip = gameObjects.FirstOrDefault(gameObject => gameObject.ObjectType == ObjectType.PlayerShip);

            if (playerShip != null && Math.Abs(PositionY - playerShip.PositionY) <= playerShip.Size && DelayOfShot <= 0)
            {
                createdObjects.Add(new BomberShipBullet(PositionX - Size, PositionY));
                ReloadWeapon();
            }

            PositionX -= 6;
        }
    }
}
