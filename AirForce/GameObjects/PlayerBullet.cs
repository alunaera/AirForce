using AirForce.Commands;

namespace AirForce
{
    internal class PlayerBullet : GameObject
    {
        public PlayerBullet(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.PlayerBullet;
            ObjectType = ObjectType.PlayerBullet;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 14;
        }

        public override void Update(Game game)
        {
            game.CommandManager.ExecuteCommand(new CommandMove(this, 8, 0));
        }
    }
}
