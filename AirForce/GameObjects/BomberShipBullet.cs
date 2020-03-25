using System.Collections.Generic;

namespace AirForce
{
    internal class BomberShipBullet : GameObject
    {
        public BomberShipBullet(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.BomberShipBullet;
            ObjectType = ObjectType.BomberShipBullet;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 14;
        }

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();
            PositionX -= 7;
        }
    }
}
