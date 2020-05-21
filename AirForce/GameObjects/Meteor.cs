using AirForce.Commands;

namespace AirForce
{
    internal class Meteor : GameObject
    {
        public Meteor(int positionX, int positionY)
        {
            Bitmap = Properties.Resources.Meteor;
            ObjectType = ObjectType.Meteor;
            PositionX = positionX;
            PositionY = positionY;
            OffsetX = -10;
            OffsetY = 4;
            Size = 160;
            Health = 10;
        }

        public override void Update(Game game)
        {
            game.CommandManager.ExecuteCommand(new CommandMove(this));
        }
    }
}
