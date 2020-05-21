using System.Collections.Generic;
using System.Linq;
using AirForce.Commands;

namespace AirForce
{
    internal class CommandManager
    {
        private readonly List<List<ICommand>> commandRosters = new List<List<ICommand>>();

        public void ExecuteCommand(ICommand command)
        {
            if (commandRosters.Count == 0)
                return;

            command.Execute();
            commandRosters.Last().Add(command);
        }

        public void CreateNewRoster()
        {
            commandRosters.Add(new List<ICommand>());
        }

        public void UndoLastRoster()
        {
            if (commandRosters.Count == 0)
                return;

            foreach (ICommand command in commandRosters.Last())
                command.Undo();

            commandRosters.RemoveAt(commandRosters.Count - 1);
        }
    }
}
