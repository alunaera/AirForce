using System.Collections.Generic;

namespace AirForce
{
    internal class PlayerBullet : GameObject
    {
        public PlayerBullet(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.PlayerBullet;
            ObjectType = ObjectType.PlayerBullet;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 14;
        }

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();
            PositionX += 8;
        }
    }
}
