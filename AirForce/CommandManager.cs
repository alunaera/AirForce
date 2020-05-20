using System.Collections.Generic;
using System.Linq;
using AirForce.Commands;

namespace AirForce
{
    internal static class CommandManager
    {
        private static readonly List<List<ICommand>> CommandRosters = new List<List<ICommand>>(); 
        public static bool IsReverse = false;

        public static void Clear()
        {
            CommandRosters.Clear();
        }

        public static void ExecuteCommand(ICommand command)
        {
            if (CommandRosters.Count <= 0 || IsReverse)
                return;

            command.Execute();
            CommandRosters.Last().Add(command);
        }

        public static void CreateNewRoster()
        {
            if (!IsReverse)
                CommandRosters.Add(new List<ICommand>());
        }

        public static void UndoLastRoster()
        {
            if (CommandRosters.Count < 1 || !IsReverse)
                return;

            foreach (ICommand command in CommandRosters.Last())
                command.Undo();

            CommandRosters.Remove(CommandRosters.Last());
        }
    }
}
