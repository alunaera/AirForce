using System.Collections.Generic;

namespace AirForce
{
    internal class Ship : ObjectOnGameField
    {
        protected const int GroundLevel = 10;

        public int DelayOfShot { get; protected set; }

        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList)
        {
            PositionX -= 6;
        }

        public void IncreaseDelayOfShot(int valueOfIncrease)
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
