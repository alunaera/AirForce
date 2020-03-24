using System.Collections.Generic;

namespace AirForce
{
    internal class Meteor : GameObject
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

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();
            PositionX -= 10;
            PositionY += 4;
        }
    }
}
