namespace AirForce.Commands
{
    internal class CommandChangeScore : ICommand
    {
        private readonly Game game;

        public CommandChangeScore(Game game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Score++;
        }

        public void Undo()
        {
            game.Score--;
        }
    }
}
