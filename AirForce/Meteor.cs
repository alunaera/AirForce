namespace AirForce
{
    internal class Meteor : IMovable
    {
        public int PositionX;
        public int PositionY;
        public int Size { get; }
        public int Health;

        public Meteor(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            Size = 160;
            Health = 5;
        }

        public void Move()
        {

        }

        public void TakeDamage<T>()
        {
            switch (typeof(T).ToString())
            {
                case "AirForce.EnemyShip":
                case "AirForce.BigShip":
                case "AirForce.PlayerShip":
                    Health--;
                    break;
                case "AirForce.Ground":
                    Health = 0;
                    break;
            }
        }
    }
}
