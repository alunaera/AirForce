using AirForce.Commands;

namespace AirForce
{
    internal class BomberShipBullet : GameObject
    {
        public BomberShipBullet(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.BomberShipBullet;
            ObjectType = ObjectType.BomberShipBullet;
            PositionX = positionX;
            PositionY = positionY;
            OffsetX = -7;
            OffsetY = 0;
            Health = 1;
            Size = 14;
        }

        public override void Update(Game game)
        {
            game.CommandManager.ExecuteCommand(new CommandMove(this));
        }
    }
}
