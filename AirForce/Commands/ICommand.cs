namespace AirForce.Commands
{
    internal interface ICommand
    {
        void Execute();

        void Undo();
    }
}
