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
            PositionX = 100;
            PositionY = 375;
            Health = 5;
            Size = 80;
            DelayOfShot = 0;
        }

        public override void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects)
        {
            createdObjects = new List<GameObject>();

            if (moveMode.HasFlag(MoveMode.Left))
                PositionX -= 8;
            if (moveMode.HasFlag(MoveMode.Right))
                PositionX += 8;
            if (moveMode.HasFlag(MoveMode.Up))
                PositionY -= 8;
            if (moveMode.HasFlag(MoveMode.Down))
                PositionY += 8;
            
            PositionX = PositionX + Size / 2 > MaxPositionX
                ? MaxPositionX - Size / 2
                : PositionX;

            PositionX = PositionX - Size / 2 < 0
                ? Size / 2
                : PositionX;

            PositionY = PositionY - Size / 2 < 0
                ? Size / 2
                : PositionY;

            IncreaseDelayOfShot(15);
        }

        public void ChangeMoveMode(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.W:
                    moveMode |= MoveMode.Up;
                    break;
                case Keys.D:
                    moveMode |= MoveMode.Right;
                    break;
                case Keys.S:
                    moveMode |= MoveMode.Down;
                    break;
                case Keys.A:
                    moveMode |= MoveMode.Left;
                    break;
            }
        }

        public void SetMoveModeDefaultValue(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.W:
                    moveMode ^= MoveMode.Up;
                    break;
                case Keys.D:
                    moveMode ^= MoveMode.Right;
                    break;
                case Keys.S:
                    moveMode ^= MoveMode.Down;
                    break;
                case Keys.A:
                    moveMode ^= MoveMode.Left;
                    break;
            }
        }
    }
}
