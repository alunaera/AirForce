using System.Collections.Generic;

namespace AirForce
{
    internal class Ship : ObjectOnGameField
    {
        public int DelayOfShot { get; protected set; }

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList, out List<ObjectOnGameField> createdObjectsList)
        {
            createdObjectsList = new List<ObjectOnGameField>();
            PositionX -= 6;
        }

        protected void IncreaseDelayOfShot(int valueOfIncrease)
        {
            if (DelayOfShot > 0)
                DelayOfShot -= valueOfIncrease;
        }

        public void SetDelayOfShotDefaultValue()
        {
            DelayOfShot = 150;
        }
    }
}
