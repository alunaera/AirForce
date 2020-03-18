using System.Collections.Generic;

namespace AirForce
{
    internal class Ship : GameObject
    {
        public int DelayOfShot { get; protected set; }

        public override void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();
            PositionX -= 6;
        }

        protected void IncreaseDelayOfShot(int valueOfIncrease)
        {
            if (DelayOfShot > 0)
                DelayOfShot -= valueOfIncrease;
        }

        public void ReloadWeapon()
        {
            DelayOfShot = 150;
        }
    }
}
