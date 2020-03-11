namespace AirForce
{
    internal class Ship : ObjectOnGameField
    {
        public override void Move()
        {
            PositionX -= 6;
        }
    }
}
