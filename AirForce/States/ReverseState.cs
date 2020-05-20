using System.Windows.Forms;

namespace AirForce.States
{
    class ReverseState : State
    {
        private int reverseSpeed;

        public ReverseState(Game game)
        {
            Game = game;
            reverseSpeed = 1;
        }

        public override void DownKey(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Left when reverseSpeed > 1:
                    reverseSpeed--;
                    break;
                case Keys.Right when reverseSpeed < 8:
                    reverseSpeed++;
                    break;
            }
        }

        public override void UpKey(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.ShiftKey:
                    Game.CurrentState = Game.NormalState;
                    Game.ClearPlayerShipsMoveMode();
                    Game.StopPlayerShipShooting();
                    reverseSpeed = 1;
                    break;
                case Keys.Left:
                    reverseSpeed = reverseSpeed > 1 ? reverseSpeed-- : reverseSpeed;
                    break;
                case Keys.Right:
                    reverseSpeed = reverseSpeed < 8 ? reverseSpeed++ : reverseSpeed;
                    break;
            }
        }

        public override void UpdateGame()
        {
            for (int i = 0; i < reverseSpeed; i++)
                Game.CommandManager.UndoLastRoster();
        }
    }
}
