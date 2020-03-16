using System;
using System.Collections.Generic;

namespace AirForce
{
    internal class Meteor : ObjectOnGameField
    {
        public Meteor(int positionX, int positionY)
        {
            ObjectType = ObjectType.Meteor;
            Bitmap = Properties.Resources.Meteor;
            PositionX = positionX;
            PositionY = positionY;
            Size = 160;
            Health = 10;
        }

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList)
        {
            PositionX -= 10;
            PositionY += 4;
        }
    }
}
