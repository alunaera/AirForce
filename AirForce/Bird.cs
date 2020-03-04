namespace AirForce
{
    internal class Bird : IMovable
    {
        public int PositionX;
        public int PositionY;

        public Bird(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void Move()
        {

        }
    }
}
