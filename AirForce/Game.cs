using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce
{
    internal class Game
    {
        private const int GroundLevel = 750;

        private int gameFieldWidth;
        private int gameFieldHeight;

        private readonly PlayerShip playerShip = new PlayerShip();
        private List<EnemyShip> enemyShipsList = new List<EnemyShip>();

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;

            playerShip.SetDefaultValue();

            enemyShipsList.Add(new EnemyShip(1500, 300));
        }

        public void Update()
        {
            playerShip.Move();

            foreach (EnemyShip enemyShip in enemyShipsList)
            {
                enemyShip.Move();

                if (enemyShip.PositionX <= 0)
                    enemyShip.DestroyShip();
            }

            enemyShipsList.Where(enemyShip => enemyShip.Health > 0);

            if (IsDefeat())
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

        private bool IsDefeat()
        {
            if (playerShip.PositionY + playerShip.Size / 2 >= GroundLevel + 5 || playerShip.Health == 0) 
                return true;

            return false;
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawShips(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.DarkGreen, 0, GroundLevel, gameFieldWidth, 100);
        }

        private void DrawShips(Graphics graphics)
        {
            graphics.DrawImage(Properties.Resources.PlayerShip,
                playerShip.PositionX - playerShip.Size / 2, playerShip.PositionY - playerShip.Size / 2, playerShip.Size,
                playerShip.Size);

            foreach (EnemyShip enemyShip in enemyShipsList)
            {
                graphics.DrawImage(Properties.Resources.EnemyShip,
                    enemyShip.PositionX - enemyShip.Size / 2, enemyShip.PositionY - enemyShip.Size / 2,
                    enemyShip.Size, enemyShip.Size);
            }
        }
    }
}
