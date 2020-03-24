using System.Collections.Generic;

namespace AirForce
{
    internal class Ground : GameObject
    {
        public int Width { get; }
        public int Height { get; }

        public Ground(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            ObjectType = ObjectType.Ground;
            Bitmap = Properties.Resources.Ground;
            Health = 1000;
            Size = 0;
            Width = 1550;
            Height = 120;
        }

        public override void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();
        }
    }
}
