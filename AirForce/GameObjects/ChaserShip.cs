using System;
using System.Linq;
using AirForce.Commands;

namespace AirForce
{
    internal class ChaserShip : ArmedGameObject
    {
        public ChaserShip(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.ChaserShip;
            ObjectType = ObjectType.ChaserShip;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 80;
        }

        public override void Update(Game game)
        {
            GameObject playerShipBullet =
                game.GameObjects.Where(gameObject => gameObject.ObjectType == ObjectType.PlayerBullet)
                    .OrderBy(gameObject => GetSqrDistanceToObject(gameObject.PositionX, gameObject.PositionY))
                    .FirstOrDefault();

            if (playerShipBullet != null && Math.Abs(PositionY - playerShipBullet.PositionY) <= Size)
                CommandManager.ExecuteCommand(new CommandMove(this, 0,
                    3 * Math.Sign(PositionY - playerShipBullet.PositionY)));

            CommandManager.ExecuteCommand(new CommandMove(this, -8, 0));
        }
    }
}
