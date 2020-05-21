using AirForce.Commands;

namespace AirForce
{
    internal class Bird : GameObject
    {
        public Bird(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.Bird;
            ObjectType = ObjectType.Bird;
            PositionX = positionX;
            PositionY = positionY;
            OffsetX = -10;
            OffsetY = 0;
            Health = 1;
            Size = 30;
        }

        public override void Update(Game game)
        {
            OffsetY = Game.Random.Next(-8, 8);
            game.CommandManager.ExecuteCommand(new CommandMove(this));
        }
    }
}
