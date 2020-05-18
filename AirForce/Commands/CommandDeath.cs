namespace AirForce.Commands
{
    internal class CommandDeath : ICommand
    {
        private readonly Game game;
        private readonly GameObject gameObject;

        public CommandDeath(Game game, GameObject gameObject)
        {
            this.game = game;
            this.gameObject = gameObject;
        }

        public void Execute()
        {
            game.GameObjects.Remove(gameObject);
        }

        public void Undo()
        {
            game.GameObjects.Add(gameObject);
        }
    }
}