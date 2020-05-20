using System.Windows.Forms;

namespace AirForce.States
{
    internal class DefeatState : State
    {
        public DefeatState(Game game)
        {
            Game = game;
        }

        public override void DownKey(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.ShiftKey:
                    Game.CurrentState = Game.ReverseState;
                    CommandManager.IsReverse = true;
                    break;
                case Keys.R:
                    Game.StartGame(Game.GameFieldWidth, Game.GameFieldHeight);
                    break;
            }
        }

        public override void UpKey(Keys keyCode)
        {
        }

        public override void UpdateGame()
        {
        }
    }
}
