using System;

namespace AirForce
{
    internal class Ship : ObjectOnGameField
    {
        // Магические значения - злое зло. Исправить.
        protected const int MaxPositionX = 1535;

        public override void Move()
        {
            PositionX -= 6;

            PositionX = PositionX + Size / 2 > MaxPositionX
                ? MaxPositionX - Size / 2
                : PositionX;
        }

        public override void TakeDamage(ObjectOnGameField objectOnGameField)
        {
            switch (objectOnGameField.ObjectType)
            {
                case ObjectType.PlayerShip:
                    Destroy();
                    break;
                case ObjectType.Ground:
                case ObjectType.Meteor:
                    Destroy();
                    break;
            }
        }
    }
}
