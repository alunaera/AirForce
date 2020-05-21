using System;
using System.Linq;
using AirForce.Commands;

namespace AirForce
{
    internal class BomberShip : ArmedGameObject
    {
        public BomberShip(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.BomberShip;
            ObjectType = ObjectType.BomberShip;
            PositionX = positionX;
            PositionY = positionY;
            OffsetX = -6;
            OffsetY = 0;
            DelayOfShot = 0;
            Health = 3;
            Size = 80;
        }

        public override void Update(Game game)
        {
            DecreaseDelayOfShot(3);

            GameObject playerShip =
                game.GameObjects.FirstOrDefault(gameObject => gameObject.ObjectType == ObjectType.PlayerShip);

            if (playerShip != null && Math.Abs(PositionY - playerShip.PositionY) <= playerShip.Size && DelayOfShot <= 0)
            {
                game.CommandManager.ExecuteCommand(new CommandCreate(game.GameObjects,
                    new BomberShipBullet(PositionX - Size, PositionY)));
                ReloadWeapon();
            }

            game.CommandManager.ExecuteCommand(new CommandMove(this));
        }
    }
}
