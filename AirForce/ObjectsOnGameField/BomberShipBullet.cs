using System.Collections.Generic;

namespace AirForce
{
    internal class BomberShipBullet : GameObject
    {
        public BomberShipBullet(int positionX, int positionY)
        {
            ObjectType = ObjectType.BomberShipBullet;
            Bitmap = Properties.Resources.BomberShipBullet;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 14;
        }

        public override void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();
            PositionX -= 7;
        }
    }
}
