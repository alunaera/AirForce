using System.Collections.Generic;

namespace AirForce
{
    internal class BomberShipBullet : ObjectOnGameField
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

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList, out List<ObjectOnGameField> createdObjectsList)
        {
            createdObjectsList = new List<ObjectOnGameField>();
            PositionX -= 7;
        }
    }
}
