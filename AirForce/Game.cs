using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    internal class Game
    {
        readonly PlayerShip playerShip = new PlayerShip();

        public void Update()
        {

        }

        public void MovePlayerShip(Keys keyCode)
        {
            playerShip.Move(keyCode);
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawPlayerShip(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.DrawImage(Properties.Resources.Ground, 0, 750);
        }

        private void DrawPlayerShip(Graphics graphics)
        {
            graphics.DrawImage(Properties.Resources.PlayerShip, playerShip.PositionX - 60, playerShip.PositionY - 60, 60, 60);
        }
    }
}
