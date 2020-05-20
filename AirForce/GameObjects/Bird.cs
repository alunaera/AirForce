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
            Health = 1;
            Size = 30;
        }

        public override void Update(Game game)
        {
            game.CommandManager.ExecuteCommand(new CommandMove(this, -10, Game.Random.Next(-8, 8)));
        }
    }
}
