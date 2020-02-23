using System.Drawing;

namespace AirForce
{
    internal class Game
    {
        PlayerShip playerShip = new PlayerShip();

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.DrawImage(Properties.Resources.Ground, 0, 750);
        }
    }
}
