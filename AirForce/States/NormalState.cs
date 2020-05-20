using System.Windows.Forms;

namespace AirForce.States
{
    internal class NormalState : State
    {
        public NormalState(Game game)
        {
            Game = game;
        }

        public override void DownKey(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.W:
                    Game.StartMovingPlayerShip(MoveMode.Up);
                    break;
                case Keys.D:
                    Game.StartMovingPlayerShip(MoveMode.Right);
                    break;
                case Keys.S:
                    Game.StartMovingPlayerShip(MoveMode.Down);
                    break;
                case Keys.A:
                    Game.StartMovingPlayerShip(MoveMode.Left);
                    break;
                case Keys.Space:
                    Game.StartPlayerShipShooting();
                    break;
                case Keys.ShiftKey:
                    Game.CurrentState = Game.ReverseState;
                    CommandManager.IsReverse = true;
                    break;
            }
        }

        public override void UpKey(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.W:
                    Game.StopMovingPlayerShip(MoveMode.Up);
                    break;
                case Keys.D:
                    Game.StopMovingPlayerShip(MoveMode.Right);
                    break;
                case Keys.S:
                    Game.StopMovingPlayerShip(MoveMode.Down);
                    break;
                case Keys.A:
                    Game.StopMovingPlayerShip(MoveMode.Left);
                    break;
                case Keys.Space:
                    Game.StopPlayerShipShooting();
                    break;
            }
        }

        public override void UpdateGame()
        {
            Game.Update();
        }
    }
}
