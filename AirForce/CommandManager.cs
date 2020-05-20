using System.Collections.Generic;
using System.Linq;
using AirForce.Commands;

namespace AirForce
{
    internal class CommandManager
    {
        private readonly List<List<ICommand>> CommandRosters = new List<List<ICommand>>();

        public void Clear()
        {
            CommandRosters.Clear();
        }

        public void ExecuteCommand(ICommand command)
        {
            if (CommandRosters.Count <= 0)
                return;

            command.Execute();
            CommandRosters.Last().Add(command);
        }

        public void CreateNewRoster()
        {
            CommandRosters.Add(new List<ICommand>());
        }

        public void UndoLastRoster()
        {
            if (CommandRosters.Count < 1)
                return;

            foreach (ICommand command in CommandRosters.Last())
                command.Undo();

            CommandRosters.Remove(CommandRosters.Last());
        }
    }
}
