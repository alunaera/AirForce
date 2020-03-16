using System.Collections.Generic;

namespace AirForce
{
    class PlayerBullet : ObjectOnGameField
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

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList)
        {
            PositionX += 8;
        }
    }
}
