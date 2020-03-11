﻿namespace AirForce
{
    internal class Bird : ObjectOnGameField
    {
        public Bird(int positionX, int positionY)
        {
            ObjectType = ObjectType.Bird;
            Bitmap = Properties.Resources.Bird;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 30;
        }

        public override void Move()
        {
            PositionX -= 10;
        }
    }
}
