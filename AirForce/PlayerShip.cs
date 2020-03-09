using System.Windows.Forms;

namespace AirForce
{
    class PlayerShip : Ship
    {
        private MoveMode MoveMode;

        public PlayerShip()
        {
            ObjectType = ObjectType.PlayerShip;
            SetDefaultValue();
        }

        private void SetDefaultValue()
        {
            PositionX = 100;
            PositionY = 375;
            Health = 3;
            Size = 80;

            SetMoveModeDefaultValue();
        }

        public new void Move()
        {
            switch (MoveMode)
            {
                case MoveMode.Top:
                    PositionY -= 8;
                    break;
                case MoveMode.Right:
                    PositionX += 8;
                    break;
                case MoveMode.Down:
                    PositionY += 8;
                    break;
                case MoveMode.Left:
                    PositionX -= 8;
                    break;
            }

            PositionX = PositionX + Size / 2 > MaxPositionX
                ? MaxPositionX - Size / 2
                : PositionX;

            PositionX = PositionX - Size / 2 < 0 
                ? Size / 2
                : PositionX;

            PositionY = PositionY - Size / 2 < 0 
                ? Size / 2 
                : PositionY;
        }

        public void ChangeMoveMode(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.W:
                    MoveMode = MoveMode.Top;
                    break;
                case Keys.D:
                    MoveMode = MoveMode.Right;
                    break;
                case Keys.S:
                    MoveMode = MoveMode.Down;
                    break;
                case Keys.A:
                    MoveMode = MoveMode.Left;
                    break;
            }
        }

        public void SetMoveModeDefaultValue()
        {
            MoveMode = MoveMode.NoMove;
        }

        public void TakeDamage<T>()
        {
            switch (typeof(T).ToString())
            {
                case "AirForce.ChaserShip":
                case "AirForce.BigShip":
                case "AirForce.Bird":
                    Health--;
                    break;
                case "AirForce.Ground":
                case "AirForce.Meteor":
                    DestroyShip();
                    break;
            }
        } 
    }
}
