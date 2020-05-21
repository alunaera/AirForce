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
            OffsetX = -8;
            OffsetY = 0;
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
                OffsetY = 3 * Math.Sign(PositionY - playerShipBullet.PositionY);
            else
                OffsetY = 0;

            game.CommandManager.ExecuteCommand(new CommandMove(this));
        }
    }
}
