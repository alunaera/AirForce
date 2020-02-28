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

        private PlayerShip playerShip;
        private List<EnemyShip> enemyShipsList;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;

            playerShip = new PlayerShip();
            enemyShipsList = new List<EnemyShip>();

            enemyShipsList.Add(new EnemyShip(1500, 300));
        }

        public void Update()
        {
            playerShip.Move();

            for (var i = 0; i < enemyShipsList.Count; i++)
            {
                EnemyShip enemyShip = enemyShipsList[i];

                if (enemyShip.Health > 0)
                {
                    enemyShip.Move();

                    if (enemyShip.PositionX <= 0)
                        enemyShip.DestroyShip();

                    int distanceToPlayerShip = GetVectorLength(playerShip.PositionX, playerShip.PositionY,
                        enemyShip.PositionX, enemyShip.PositionY);

                    if (distanceToPlayerShip >= playerShip.Size / 2 + enemyShip.Size / 2)
                        continue;
                    playerShip.TakeDamage();
                    enemyShip.DestroyShip();
                }
                else
                {
                    enemyShipsList.RemoveAt(i);
                    i--;
                }
            }

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

        private int GetVectorLength(int startX, int startY, int finishX, int finishY)
        {
            double componentX = Math.Pow(finishX - startX, 2);
            double componentY = Math.Pow(finishY - startY, 2);

            return  (int)Math.Sqrt(componentX + componentY);
        } 

        private bool IsDefeat()
        {
            if (playerShip.PositionY + playerShip.Size / 2 < GroundLevel + 5 && playerShip.Health > 0) 
                return false;

            return true;
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
