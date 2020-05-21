using AirForce.Commands;

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
            Bitmap = Properties.Resources.PlayerShip;
            ObjectType = ObjectType.PlayerShip;
            PositionX = 100;
            PositionY = 375;
            Health = 5;
            Size = 80;
            DelayOfShot = 0;
            isPlayerShooting = false;
        }

        public override void Update(Game game)
        {
            OffsetX = 0;
            OffsetY = 0;

            if (moveMode.HasFlag(MoveMode.Left))
                OffsetX = -8;
            if (moveMode.HasFlag(MoveMode.Right))
                OffsetX = 8;
            if (moveMode.HasFlag(MoveMode.Up))
                OffsetY = -8;
            if (moveMode.HasFlag(MoveMode.Down))
                OffsetY = 8;

            game.CommandManager.ExecuteCommand(new CommandMove(this));

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
                game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects, new PlayerBullet(PositionX + Size / 2, PositionY)));
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

        public void ClearMoveMode()
        {
            moveMode = 0;
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
