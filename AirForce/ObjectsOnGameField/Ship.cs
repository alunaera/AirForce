namespace AirForce
{
    internal class Ship : ObjectOnGameField
    {
        public int DelayOfShot { get; protected set; }

        public override void Move()
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
