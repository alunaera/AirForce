namespace AirForce.Commands
{
    class CommandMove : ICommand
    {
        private readonly GameObject gameObject;
        private readonly int offsetX;
        private readonly int offsetY;

        public CommandMove(GameObject gameObject, int offsetX, int offsetY)
        {
            this.gameObject = gameObject;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public void Execute()
        {
            gameObject.PositionX += offsetX;
            gameObject.PositionY += offsetY;
        }

        public void Undo()
        {
            gameObject.PositionX -= offsetX;
            gameObject.PositionY -= offsetY;
        }
    }
}
