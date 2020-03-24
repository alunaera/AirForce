using System.Collections.Generic;

namespace AirForce
{
    internal class PlayerShip : ArmedGameObject
    {
        private readonly int maxPositionX;

        private MoveMode moveMode;
        private bool isPlayerShooting;

        public PlayerShip(int maxPositionX)
        {
            this.maxPositionX = maxPositionX;
            ObjectType = ObjectType.PlayerShip;
            Bitmap = Properties.Resources.PlayerShip;
            PositionX = 100;
            PositionY = 375;
            Health = 5;
            Size = 80;
            DelayOfShot = 0;
            isPlayerShooting = false;
        }

        public override void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects)
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

            PositionX = PositionX + Size / 2 > maxPositionX
                ? maxPositionX - Size / 2
                : PositionX;

            PositionX = PositionX - Size / 2 < 0
                ? Size / 2
                : PositionX;

            PositionY = PositionY - Size / 2 < 0
                ? Size / 2
                : PositionY;

            DecreaseDelayOfShot(15);

            if (isPlayerShooting && DelayOfShot <= 0)
            {
                createdObjects.Add(new PlayerBullet(PositionX + Size / 2, PositionY));
                ReloadWeapon();
            }
        }

        public void StartMoving(MoveMode moveMode)
        {
            switch (moveMode)
            {
                case MoveMode.Up:
                    this.moveMode |= MoveMode.Up;
                    break;
                case MoveMode.Right:
                    this.moveMode |= MoveMode.Right;
                    break;
                case MoveMode.Down:
                    this.moveMode |= MoveMode.Down;
                    break;
                case MoveMode.Left:
                    this.moveMode |= MoveMode.Left;
                    break;
            }
        }

        public void StopMoving(MoveMode moveMode)
        {
            switch (moveMode)
            {
                case MoveMode.Up:
                    this.moveMode ^= MoveMode.Up;
                    break;
                case MoveMode.Right:
                    this.moveMode ^= MoveMode.Right;
                    break;
                case MoveMode.Down:
                    this.moveMode ^= MoveMode.Down;
                    break;
                case MoveMode.Left:
                    this.moveMode ^= MoveMode.Left;
                    break;
            }
        }

        public void StartShooting()
        {
            isPlayerShooting = true;
        }

        public void StopShooting()
        {
            isPlayerShooting = false;
        }
    }
}
