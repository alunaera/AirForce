using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce
{
    internal class Game
    {
        public static readonly Random Random = new Random();
        private readonly Font font = new Font("Arial", 15);

        private int gameFieldWidth;
        private int gameFieldHeight;
        private int score;
        private PlayerShip PlayerShip => (PlayerShip)gameObjects[0];
        private Ground ground;
        private List<GameObject> gameObjects;
        private List<Point> blasts;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;
            score = 0;

            ground = new Ground();
            gameObjects = new List<GameObject> {new PlayerShip()};
            blasts = new List<Point>();
        }

        public void Update()
        {
            if (IsDefeat())
                Defeat();

            blasts.Clear();

            MoveObjectsOnGameField();

            if (gameObjects.Count(gameObject => gameObject.ObjectType != ObjectType.PlayerBullet) < 2)
                GenerateEnemies();
        }

        private void MoveObjectsOnGameField()
        { 
            List<GameObject> createdObjects = new List<GameObject>();

            foreach (GameObject gameObject in gameObjects)
                gameObject.Move(gameObjects, out createdObjects);

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.ObjectType != ObjectType.PlayerBullet)
                {
                    if (gameObject.PositionX + gameObject.Size < 0)
                        gameObject.Destroy();
                }
                else
                {
                    if (gameObject.PositionX + gameObject.Size > gameFieldWidth)
                        gameObject.Destroy();
                }

                if (gameObject.PositionY + gameObject.Size / 2 >= ground.PositionY + 5)
                    gameObject.TakeDamage(ground.Health);

                if (gameObject.Health > 0)
                {
                    var damageableObjects =
                        gameObjects.Where(nextGameObject => IsDamaged(gameObject, nextGameObject));

                    foreach (GameObject nextGameObject in damageableObjects)
                    {
                        int gameObjectHealth = gameObject.Health;

                        gameObject.TakeDamage(nextGameObject.Health);
                        nextGameObject.TakeDamage(gameObjectHealth);

                        if (gameObject.ObjectType != ObjectType.PlayerBullet &&
                            gameObject.ObjectType != ObjectType.PlayerShip ||
                            nextGameObject.ObjectType != ObjectType.BomberShip &&
                            nextGameObject.ObjectType != ObjectType.ChaserShip ||
                            nextGameObject.Health >= 1)
                            continue;

                        score++;
                    }
                }
                else
                {
                    blasts.Add(new Point(gameObject.PositionX, gameObject.PositionY));
                }
            }

            gameObjects.RemoveAll(gameObject => gameObject.Health <= 0 && gameObject.ObjectType != ObjectType.PlayerShip);
            gameObjects.AddRange(createdObjects);
        }

        public void ChangePlayerShipMoveMode(Keys keyCode)
        {
            PlayerShip.ChangeMoveMode(keyCode);
        }

        public void MakeShot()
        {
            if (PlayerShip.DelayOfShot > 0) 
                return;

            gameObjects.Add(new PlayerBullet(PlayerShip.PositionX + PlayerShip.Size / 2,  PlayerShip.PositionY));
            PlayerShip.ReloadWeapon();
        }

        public void SetPlayerShipMoveModeDefaultValue(Keys keyCode)
        {
            PlayerShip.SetMoveModeDefaultValue(keyCode);
        }

        private void GenerateEnemies()
        {
            int generatedObjectsCount = Random.Next(1, 3);

            for (int i = 0; i < generatedObjectsCount; i++)
                switch (Random.Next(1, 5))
                {
                    case 1:
                        gameObjects.Add(new ChaserShip(1500, Random.Next(100, 500)));
                        break;
                    case 2:
                        gameObjects.Add(new BomberShip(1500, Random.Next(100, 500)));
                        break;
                    case 3:
                        gameObjects.Add(new Meteor(Random.Next(1400, 1500), 0));
                        break;
                    case 4:
                        gameObjects.Add(new Bird(1500, Random.Next(500, 700)));
                        break;
                }
        }

        private bool IsDefeat()
        {
            return PlayerShip.Health < 1;
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawInterface(graphics);
            DrawObjectsOnGameField(graphics);
            DrawBlast(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.LightCyan, 0, 0, gameFieldWidth, gameFieldHeight);
            graphics.DrawImage(ground.Bitmap, ground.PositionX, ground.PositionY, ground.Width, ground.Height);
        }

        private void DrawInterface(Graphics graphics)
        {
            graphics.DrawString("Score: " + score, font, Brushes.Black, 1370, 10);
            graphics.DrawString("Player's health: " + PlayerShip.Health, font, Brushes.Black, 1370, 30);
        }

        private void DrawBlast(Graphics graphics)
        {
            foreach (Point blast in blasts)
                graphics.DrawImage(Properties.Resources.Blast, blast.X - 5, blast.Y - 5, 10, 10);

        }

        private void DrawObjectsOnGameField(Graphics graphics)
        {
            graphics.DrawImage(PlayerShip.Bitmap,
                PlayerShip.PositionX - PlayerShip.Size / 2, PlayerShip.PositionY - PlayerShip.Size / 2,
                PlayerShip.Size, PlayerShip.Size);

            foreach (GameObject objectOnGameField in gameObjects)
                graphics.DrawImage(objectOnGameField.Bitmap,
                    objectOnGameField.PositionX - objectOnGameField.Size / 2,
                    objectOnGameField.PositionY - objectOnGameField.Size / 2,
                    objectOnGameField.Size, objectOnGameField.Size);
        }

        private readonly Dictionary<ObjectType, ObjectType> intersectTable = new Dictionary<ObjectType, ObjectType>
        {
            [ObjectType.PlayerShip] = ObjectType.ChaserShip |
                                      ObjectType.BomberShip |
                                      ObjectType.BomberShipBullet |
                                      ObjectType.Meteor |
                                      ObjectType.Bird |
                                      ObjectType.Ground,

            [ObjectType.PlayerBullet] = ObjectType.ChaserShip |
                                        ObjectType.BomberShip |
                                        ObjectType.Meteor |
                                        ObjectType.Ground,

            [ObjectType.ChaserShip] = ObjectType.PlayerShip |
                                      ObjectType.PlayerBullet |
                                      ObjectType.Meteor |
                                      ObjectType.Ground,

            [ObjectType.BomberShip] = ObjectType.PlayerShip |
                                      ObjectType.PlayerBullet |
                                      ObjectType.Meteor |
                                      ObjectType.Ground,

            [ObjectType.BomberShipBullet] = ObjectType.PlayerShip |
                                            ObjectType.Meteor |
                                            ObjectType.Ground,

            [ObjectType.Meteor] = ObjectType.PlayerShip |
                                  ObjectType.PlayerBullet |
                                  ObjectType.ChaserShip |
                                  ObjectType.BomberShip |
                                  ObjectType.BomberShipBullet |
                                  ObjectType.Ground,

            [ObjectType.Bird] = ObjectType.PlayerShip

        };

        private bool IsDamaged(GameObject a, GameObject b)
        {
            return intersectTable[a.ObjectType].HasFlag(b.ObjectType) &&
                   a.IsIntersection(b) &&
                   b.Health > 0;
        }
    }
}
