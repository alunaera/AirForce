namespace AirForce.Commands
{
    class CommandMove : ICommand
    {
        private readonly GameObject gameObject;
        private readonly int offsetX;
        private readonly int offsetY;

        public CommandMove(GameObject gameObject)
        {
            this.gameObject = gameObject;
            offsetX = gameObject.OffsetX;
            offsetY = gameObject.OffsetY;
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
