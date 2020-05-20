using System.Windows.Forms;

namespace AirForce.States
{
    internal abstract class State
    {
        protected Game Game;

        public abstract void DownKey(Keys keyCode);

        public abstract void UpKey(Keys keyCode);

        public abstract void UpdateGame();
    }
}
