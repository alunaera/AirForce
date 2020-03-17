using System.Collections.Generic;

namespace AirForce
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

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList, out List<ObjectOnGameField> createdObjectsList)
        {
            createdObjectsList = new List<ObjectOnGameField>();
            PositionX -= 10;
        }
    }
}
