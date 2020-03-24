using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
        private List<Blast> blasts;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;
            score = 0;

            ground = new Ground(0, gameFieldHeight - 120);
            gameObjects = new List<GameObject> {new PlayerShip(gameFieldWidth)};
            blasts = new List<Blast>();
        }

        public void Update()
        {
            foreach (Blast blast in blasts)
                blast.SetNextStage();

            blasts.RemoveAll(blast => blast.StageOfBlast > 3);

            MoveGameObjects();

            if (gameObjects.Count(gameObject =>
                                  gameObject.ObjectType != ObjectType.PlayerBullet &&
                                  gameObject.ObjectType != ObjectType.BomberShipBullet) < 2)
                GenerateEnemies();

            if (PlayerShip.Health < 1)
                Defeat();
        }

        private void MoveGameObjects()
        { 
            List<GameObject> createdObjects = new List<GameObject>();

            foreach (GameObject gameObject in gameObjects)
                gameObject.Update(gameObjects, out createdObjects);

            gameObjects.RemoveAll(gameObject => gameObject.ObjectType != ObjectType.PlayerShip &&
                                                    (gameObject.PositionX + gameObject.Size / 2 < 0 ||
                                                     gameObject.PositionX > gameFieldWidth));

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.PositionY + gameObject.Size / 2 >= ground.PositionY + 5 &&
                    CanIntersect(gameObject, ground))
                {
                    gameObject.Destroy();
                    blasts.Add(new Blast(new Point(gameObject.PositionX, gameObject.PositionY + gameObject.Size / 2)));
                    continue;
                }

                foreach (GameObject nextGameObject in gameObjects)
                {
                    if (CanIntersect(gameObject, nextGameObject) &&
                        gameObject.IntersectsWith(nextGameObject) &&
                        nextGameObject.Health > 0)
                    {
                        int gameObjectHealth = gameObject.Health;

                        gameObject.TakeDamage(nextGameObject.Health);
                        nextGameObject.TakeDamage(gameObjectHealth);

                        if (gameObject.Health <= 0 || nextGameObject.Health <= 0)
                            blasts.Add(new Blast(gameObject.GetMiddleOfVector(nextGameObject)));

                        switch (gameObject.ObjectType)
                        {
                            case ObjectType.PlayerShip:
                            case ObjectType.PlayerBullet:
                                if (nextGameObject.Health <= 0 &&
                                    (nextGameObject.ObjectType == ObjectType.BomberShip ||
                                     nextGameObject.ObjectType == ObjectType.ChaserShip))
                                    score++;
                                break;
                            case ObjectType.ChaserShip:
                            case ObjectType.BomberShip:
                                if (gameObject.Health <= 0 &&
                                    nextGameObject.ObjectType == ObjectType.PlayerBullet)
                                    score++;
                                break;
                        }
                    }
                }
            }

            gameObjects.RemoveAll(gameObject => gameObject.Health <= 0 && gameObject.ObjectType != ObjectType.PlayerShip);
            gameObjects.AddRange(createdObjects);
        }

        public void StartMovingPlayerShip(MoveMode moveMode)
        {
            PlayerShip.StartMoving(moveMode);
        }

        public void StopMovingPlayerShip(MoveMode moveMode)
        {
            PlayerShip.StopMoving(moveMode);
        }

        public void StartPlayerShipShooting()
        {
            PlayerShip.StartShooting();
        }

        public void StopPlayerShipShooting()
        {
            PlayerShip.StopShooting();
        }

        private void GenerateEnemies()
        {
            int generatedObjectsCount = Random.Next(1, 3);

            for (int i = 0; i < generatedObjectsCount; i++)
                switch (Random.Next(1, 5))
                {
                    case 1:
                        gameObjects.Add(new ChaserShip(gameFieldWidth, Random.Next(100, gameFieldHeight - 350)));
                        break;
                    case 2:
                        gameObjects.Add(new BomberShip(gameFieldWidth, Random.Next(100, gameFieldHeight - 350)));
                        break;
                    case 3:
                        gameObjects.Add(new Meteor(Random.Next(gameFieldWidth - 100, gameFieldWidth), 0));
                        break;
                    case 4:
                        gameObjects.Add(new Bird(gameFieldWidth, Random.Next(gameFieldHeight - 300, gameFieldHeight - 50)));
                        break;
                }
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
            graphics.DrawString("Score: " + score, font, Brushes.Black, gameFieldWidth - 165, 10);
            graphics.DrawString("Player's health: " + PlayerShip.Health, font, Brushes.Black, gameFieldWidth - 165, 30);
        }

        private void DrawBlast(Graphics graphics)
        {
            foreach (Blast blast in blasts)
                blast.Draw(graphics);
        }

        private void DrawObjectsOnGameField(Graphics graphics)
        {
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

        private bool CanIntersect(GameObject firstGameObject, GameObject secondGameObject)
        {
            return intersectTable[firstGameObject.ObjectType].HasFlag(secondGameObject.ObjectType);
        }
    }
}
