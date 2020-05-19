namespace AirForce.Commands
{
    internal class CommandCreate : ICommand
    {
        private readonly Game game;
        private readonly GameObject gameObject;

        public CommandCreate(Game game, GameObject gameObject)
        {
            this.game = game;
            this.gameObject = gameObject;
        }

        public void Execute()
        {
            game.GameObjects.Add(gameObject);
        }

        public void Undo()
        {
            game.GameObjects.Remove(gameObject);
        }
    }
}
