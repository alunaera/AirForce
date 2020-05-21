using System.Collections.Generic;

namespace AirForce.Commands
{
    internal class CommandDeath : ICommand
    {
        private readonly List<GameObject> gameObjects;
        private readonly GameObject gameObject;

        public CommandDeath(List<GameObject> gameObjects, GameObject gameObject)
        {
            this.gameObjects = gameObjects;
            this.gameObject = gameObject;
        }

        public void Execute()
        {
            gameObjects.Remove(gameObject);
        }

        public void Undo()
        {
            gameObjects.Add(gameObject);
        }
    }
}