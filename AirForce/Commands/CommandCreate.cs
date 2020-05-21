using System.Collections.Generic;

namespace AirForce.Commands
{
    internal class CommandCreate : ICommand
    {
        private readonly List<GameObject> gameObjects;
        private readonly GameObject gameObject;

        public CommandCreate(List<GameObject> gameObjects, GameObject gameObject)
        {
            this.gameObjects = gameObjects;
            this.gameObject = gameObject;
        }

        public void Execute()
        {
            gameObjects.Add(gameObject);
        }

        public void Undo()
        {
            gameObjects.Remove(gameObject);
        }
    }
}
