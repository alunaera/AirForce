namespace AirForce.Commands
{
    internal class CommandTakeDamage : ICommand
    {
        private readonly GameObject gameObject;
        private readonly int damage;

        public CommandTakeDamage(GameObject gameObject, int damage)
        {
            this.gameObject = gameObject;
            this.damage = damage;
        }

        public void Execute()
        {
            gameObject.Health -= damage;
        }

        public void Undo()
        {
            gameObject.Health += damage;
        }
    }
}
