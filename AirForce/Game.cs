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
            switch (keyCode)
                {
                    case Keys.W:
                        playerShip.MoveMode = MoveMode.Top;
                        break;
                    case Keys.D:
                        playerShip.MoveMode = MoveMode.Right;
                        break;
                    case Keys.S:
                        playerShip.MoveMode = MoveMode.Down;
                        break;
                    case Keys.A:
                        playerShip.MoveMode = MoveMode.Left;
                        break;
                }
        }

        public void SetPlayerShipMoveModeDefaultValue()
        {
            playerShip.MoveMode = MoveMode.NoMove;
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
