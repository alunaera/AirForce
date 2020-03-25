using System.Collections.Generic;

namespace AirForce
{
    internal class Bird : GameObject
    {
        public Bird(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.Bird;
            ObjectType = ObjectType.Bird;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 30;
        }

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();

            PositionX -= 10;
            PositionY += Game.Random.Next(-8, 8);
        }
    }
}
