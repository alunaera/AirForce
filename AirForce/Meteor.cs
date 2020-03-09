using System;

namespace AirForce
{
    internal class Meteor : ObjectOnGameField
    {
        public Meteor(int positionX, int positionY)
        {
            TypeOfObject = TypeOfObject.Meteor;
            PositionX = positionX;
            PositionY = positionY;
            Size = 160;
            Health = 5;
        }

        public void Move()
        {
            PositionX -= 10;
            PositionY += 4;
        }

        public void Destroy()
        {
            Health = 0;
        }
    }
}
