namespace AirForce
{
    internal class Ground : ObjectOnGameField
    {
        public Ground()
        {
            ObjectType = ObjectType.Ground;
            PositionX = 0;
            PositionY = 750;
        }

        public override void Move() { }

        public override void TakeDamage(ObjectOnGameField objectOnGameField) { }
    }
}
