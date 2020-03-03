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

        private static readonly Random random = new Random();

        private int gameFieldWidth;
        private int gameFieldHeight;
        private PlayerShip playerShip;
        private Ground ground;
        private List<EnemyShip> enemyShipsList;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;

            playerShip = new PlayerShip();
            ground = new Ground();
            enemyShipsList = new List<EnemyShip>();

            enemyShipsList.Add(new EnemyShip(1500, 300));
        }

        public void Update()
        {
            MovePlayerShip();
            
            for (var i = 0; i < enemyShipsList.Count; i++)
            {
                EnemyShip enemyShip = enemyShipsList[i];

                if (enemyShip.Health > 0)
                {
                    enemyShip.Move();

                    if (enemyShip.PositionX + enemyShip.Size < 0)
                        enemyShip.DestroyShip();

                    if (enemyShip.IsIntersectionWithPlayerShip(playerShip))
                    {
                        playerShip.TakeDamage<EnemyShip>();
                        enemyShip.TakeDamage<PlayerShip>();
                    }
                }
                else
                {
                    enemyShipsList.RemoveAt(i);
                    i--;
                }
            }

            if (IsDefeat())
                Defeat();

            if(enemyShipsList.Count == 0)
                GenerateEnemies();
        }

        private void MovePlayerShip()
        {
            playerShip.Move();

            if (playerShip.PositionY + playerShip.Size / 2 >= GroundLevel + 5)
                playerShip.TakeDamage<Ground>();
        }

        public void ChangePlayerShipMoveMode(Keys keyCode)
        {
            playerShip.ChangeMoveMode(keyCode);
        }

        public void SetPlayerShipMoveModeDefaultValue()
        {
            playerShip.SetMoveModeDefaultValue();
        }

        private void GenerateEnemies()
        {
            enemyShipsList.Add(new EnemyShip(1500, random.Next(100, 500)));
        }

        private bool IsDefeat()
        {
            return playerShip.Health < 1;
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
