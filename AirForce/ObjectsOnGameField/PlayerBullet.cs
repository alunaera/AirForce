using System.Collections.Generic;

namespace AirForce
{
    internal class PlayerBullet : ObjectOnGameField
    {
        public PlayerBullet(int positionX, int positionY)
        {
            ObjectType = ObjectType.PlayerBullet;
            Bitmap = Properties.Resources.PlayerBullet;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 14;
        }

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList, out List<ObjectOnGameField> createdObjectsList)
        {
            createdObjectsList = new List<ObjectOnGameField>();
            PositionX += 8;
        }
    }
}
