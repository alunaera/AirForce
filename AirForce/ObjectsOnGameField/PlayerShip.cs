using System.Collections.Generic;

namespace AirForce
{
    internal class PlayerShip : Ship
    {
        private const int MaxPositionX = 1535;

        public MoveMode MoveMode;

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

            if (MoveMode.HasFlag(MoveMode.Left))
                PositionX -= 8;
            if (MoveMode.HasFlag(MoveMode.Right))
                PositionX += 8;
            if (MoveMode.HasFlag(MoveMode.Up))
                PositionY -= 8;
            if (MoveMode.HasFlag(MoveMode.Down))
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
    }
}
