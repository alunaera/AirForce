using System.Collections.Generic;
using System.Windows.Forms;

namespace AirForce
{
    internal class PlayerShip : Ship
    {
        private const int MaxPositionX = 1535;

        private MoveMode moveMode;

        public PlayerShip()
        {
            ObjectType = ObjectType.PlayerShip;
            Bitmap = Properties.Resources.PlayerShip;
            DelayOfShot = 0;
            SetDefaultValue();
        }

        private void SetDefaultValue()
        {
            PositionX = 100;
            PositionY = 375;
            Health = 5;
            Size = 80;

            SetMoveModeDefaultValue();
        }
        
        public override void Move(List<ObjectOnGameField> objectOnGameFieldsList)
        {
            switch (moveMode)
            {
                case MoveMode.Up:
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
                    moveMode = MoveMode.Up;
                    break;
                case Keys.D:
                    moveMode = MoveMode.Right;
                    break;
                case Keys.S:
                    moveMode = MoveMode.Down;
                    break;
                case Keys.A:
                    moveMode = MoveMode.Left;
                    break;
            }
        }

        public void SetMoveModeDefaultValue()
        {
            moveMode = MoveMode.NoMove;
        }
    }
}
