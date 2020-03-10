namespace AirForce
{
    internal class Ship : ObjectOnGameField
    {
        public override void Move()
        {
            PositionX -= 6;
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
