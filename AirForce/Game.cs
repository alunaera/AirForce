using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce
{
    internal class Game
    {
        private static readonly Random random = new Random();

        private int gameFieldWidth;
        private int gameFieldHeight;
        private PlayerShip playerShip;
        private Ground ground;
        private List<ChaserShip> chaserShipsList;
        private List<Meteor> meteorsList;
        private List<Bird> birdsList;
        private List<ObjectOnGameField> objectsOnGameFieldList;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;

            playerShip = new PlayerShip();
            ground = new Ground();
            chaserShipsList = new List<ChaserShip>();
            meteorsList = new List<Meteor>();
            birdsList = new List<Bird>();

            chaserShipsList.Add(new ChaserShip(1500, 300));
        }

        public void Update()
        {
            MovePlayerShip();
            MoveChaserShips();
            MoveMeteors();
            MoveBirds();

            if (IsDefeat())
                Defeat();

            if (chaserShipsList.Count + meteorsList.Count == 0)
                GenerateEnemies();

            if (birdsList.Count == 0)
                birdsList.Add(new Bird(random.Next(1400, 1500), random.Next(0, ground.PositionY - 20)));
        }

        private void MovePlayerShip()
        {
            playerShip.Move();

            if (playerShip.PositionY + playerShip.Size / 2 >= ground.PositionY + 5)
                playerShip.TakeDamage<Ground>();
        }

        private void MoveChaserShips()
        {
            for (int i = 0; i < chaserShipsList.Count; i++)
            {
                ChaserShip chaserShip = chaserShipsList[i];

                if (chaserShip.Health > 0)
                {
                    chaserShip.Move();

                    if (chaserShip.PositionX + chaserShip.Size < 0)
                        chaserShip.DestroyShip();

                    if (chaserShip.IsIntersection(playerShip.PositionX, playerShip.PositionY, playerShip.Size))
                    {
                        playerShip.TakeDamage<ChaserShip>();
                        chaserShip.TakeDamage<PlayerShip>();
                    }
                }
                else
                {
                    chaserShipsList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void MoveMeteors()
        {
            for (int i = 0; i < meteorsList.Count; i++)
            {
                Meteor meteor = meteorsList[i];

                if (meteor.Health > 0)
                {
                    meteor.Move();

                    if (meteor.PositionX + meteor.Size < 0)
                        meteor.Destroy();

                    if (meteor.IsIntersection(playerShip.PositionX, playerShip.PositionY, playerShip.Size))
                        playerShip.TakeDamage<ChaserShip>();

                    var intersectedEnemyShips = chaserShipsList.Where(chaserShip =>
                        meteor.IsIntersection(chaserShip.PositionX, chaserShip.PositionY, chaserShip.Size));

                    foreach (ChaserShip chaserShip in intersectedEnemyShips)
                        chaserShip.TakeDamage<Meteor>();
                }
                else
                {
                    meteorsList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void MoveBirds()
        {
            for (int i = 0; i < birdsList.Count; i++)
            {
                Bird bird = birdsList[i];

                if (bird.Health > 0)
                {
                    bird.Move();

                    if (bird.PositionX + bird.Size < 0)
                        bird.DestroyBird();

                    if (bird.IsIntersection(playerShip.PositionX, playerShip.PositionY, playerShip.Size))
                    {
                        playerShip.TakeDamage<Bird>();
                        bird.TakeDamage<PlayerShip>();
                    }
                }
                else
                {
                    birdsList.RemoveAt(i);
                    i--;
                }
            }
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
            switch (random.Next(1, 3))
            {
                case 1:
                    chaserShipsList.Add(new ChaserShip(1500, random.Next(100, 500)));
                    break;
                case 2:
                    meteorsList.Add(new Meteor(random.Next(1400, 1500), 0));
                    break;
            }
        }

        private bool IsDefeat()
        {
            return playerShip.Health < 1;
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawShips(graphics);
            DrawMeteors(graphics);
            DrawBirds(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.LightBlue, 0, 0, gameFieldWidth, gameFieldHeight);
            graphics.FillRectangle(Brushes.DarkGreen, ground.PositionX, ground.PositionY, gameFieldWidth, 100);
        }

        private void DrawShips(Graphics graphics)
        {
            graphics.DrawImage(Properties.Resources.PlayerShip,
                playerShip.PositionX - playerShip.Size / 2, playerShip.PositionY - playerShip.Size / 2,
                playerShip.Size, playerShip.Size);

            foreach (ChaserShip chaserShip in chaserShipsList)
                graphics.DrawImage(Properties.Resources.ChaserShip,
                    chaserShip.PositionX - chaserShip.Size / 2, chaserShip.PositionY - chaserShip.Size / 2,
                    chaserShip.Size, chaserShip.Size);
        }

        private void DrawMeteors(Graphics graphics)
        {
            foreach (Meteor meteor in meteorsList)
                graphics.DrawImage(Properties.Resources.Meteor, meteor.PositionX - meteor.Size / 2,
                    meteor.PositionY - meteor.Size / 2,
                    meteor.Size, meteor.Size);
        }

        private void DrawBirds(Graphics graphics)
        {
            foreach (Bird bird in birdsList)
                graphics.DrawImage(Properties.Resources.Bird, bird.PositionX - bird.Size / 2,
                    bird.PositionY - bird.Size / 2,
                    bird.Size, bird.Size);
        }

        private readonly Dictionary<ObjectType, Dictionary<ObjectType, bool>> intersectTable =  new Dictionary<ObjectType, Dictionary<ObjectType, bool>>
            {
                {
                    ObjectType.PlayerShip, new Dictionary<ObjectType, bool>
                    {
                        [ObjectType.ChaserShip] = true,
                        [ObjectType.BomberShip] = true,
                        [ObjectType.Meteor] = true,
                        [ObjectType.Bird] = true,
                        [ObjectType.Ground] = true

                    }
                },

                {
                    ObjectType.ChaserShip, new Dictionary<ObjectType, bool>
                    {
                        [ObjectType.PlayerShip] = true,
                        [ObjectType.BomberShip] = false,
                        [ObjectType.Meteor] = true,
                        [ObjectType.Bird] = false,
                        [ObjectType.Ground] = true

                    }
                },

                {
                    ObjectType.BomberShip, new Dictionary<ObjectType, bool>
                    {
                        [ObjectType.PlayerShip] = true,
                        [ObjectType.ChaserShip] = false,
                        [ObjectType.Meteor] = true,
                        [ObjectType.Bird] = false,
                        [ObjectType.Ground] = true

                    }
                },

                {
                    ObjectType.Meteor, new Dictionary<ObjectType, bool>
                    {
                        [ObjectType.PlayerShip] = true,
                        [ObjectType.ChaserShip] = true,
                        [ObjectType.BomberShip] = true,
                        [ObjectType.Bird] = false,
                        [ObjectType.Ground] = true

                    }
                },

                {
                    ObjectType.Bird, new Dictionary<ObjectType, bool>
                    {
                        [ObjectType.PlayerShip] = true,
                        [ObjectType.ChaserShip] = false,
                        [ObjectType.BomberShip] = false,
                        [ObjectType.Meteor] = false,
                        [ObjectType.Ground] = false

                    }
                }
            };

        public bool IsDamaged(ObjectOnGameField firstObjectOnGame, ObjectOnGameField secondObjectOnGameField)
        {
            return intersectTable[firstObjectOnGame.ObjectType][secondObjectOnGameField.ObjectType];
        }
    }
}
