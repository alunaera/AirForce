using System;
using System.Collections.Generic;

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

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.ObjectType == ObjectType.PlayerShip &&
                    Math.Abs(PositionY - gameObject.PositionY) <= gameObject.Size && DelayOfShot == 0)
                {
                    createdObjects.Add(new BomberShipBullet(PositionX - Size, PositionY));
                    ReloadWeapon();
                }
                else
                    break;
            }

            PositionX -= 6;
        }
    }
}
