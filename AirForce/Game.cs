using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    internal class Game
    {
        private const int GroundLevel = 750;

        private readonly PlayerShip playerShip = new PlayerShip();

        public event Action Defeat = delegate { };

        public void StartGame()
        {
            playerShip.SetDefaultValue();
        }

        public void Update()
        {
            playerShip.Move();
            
            if (playerShip.PositionY + playerShip.Size / 2 >= GroundLevel + 5)
                Defeat();
        }

        public void ChangePlayerShipMoveMode(Keys keyCode)
        {
            playerShip.ChangeMoveMode(keyCode);
        }

        public void SetPlayerShipMoveModeDefaultValue()
        {
            playerShip.SetMoveModeDefaultValue();
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawPlayerShip(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            // Спросить у Семена почему с картинкой падает фпс
            // graphics.DrawImage(Properties.Resources.Ground, 0, GroundLevel);

           graphics.FillRectangle(Brushes.DarkGreen, 0, GroundLevel, 1920, 100);
        }

        private void DrawPlayerShip(Graphics graphics)
        {
            graphics.DrawImage(Properties.Resources.PlayerShip,
                playerShip.PositionX - playerShip.Size / 2, playerShip.PositionY - playerShip.Size / 2, playerShip.Size, playerShip.Size);
        }
    }
}
