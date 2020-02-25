using System;
using System.Windows.Forms;

namespace AirForce
{
    class PlayerShip : Ship
    {
        public MoveMode MoveMode;
        public int Size = 80;

        public PlayerShip()
        {
            SetDefaultValue();
        }

        public void SetDefaultValue()
        {
            MoveMode = MoveMode.NoMove;
            Health = 3;
            PositionX = 100;
            PositionY = 200;
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
        }
    }
}
