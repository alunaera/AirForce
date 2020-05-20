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
            Health = 1;
            Size = 14;
        }

        public override void Update(Game game)
        {
            CommandManager.ExecuteCommand(new CommandMove(this, -7, 0));
        }
    }
}
