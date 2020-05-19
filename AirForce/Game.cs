using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AirForce.Commands;

namespace AirForce
{
    internal class Game
    {
        public static readonly Random Random = new Random();
        public List<GameObject> GameObjects;
        public int Score;

        private readonly Font font = new Font("Arial", 15);
        private CommandManager commandManager;

        private int gameFieldWidth;
        private int gameFieldHeight;
        private PlayerShip PlayerShip => (PlayerShip)GameObjects[0];
        private Ground ground;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;
            Score = 0;

            commandManager = new CommandManager();
            ground = new Ground(0, gameFieldHeight - 120);
            GameObjects = new List<GameObject> {new PlayerShip(gameFieldWidth)};
        }

        public void Update()
        {
            commandManager.CreateNewRoster();
            UpdateGameObjects();

            if (GameObjects.Count(gameObject =>
                                  gameObject.ObjectType != ObjectType.PlayerBullet &&
                                  gameObject.ObjectType != ObjectType.BomberShipBullet &&
                                  gameObject.ObjectType != ObjectType.Blast) < 2)
                GenerateEnemies();

            if (PlayerShip.Health < 1)
                Defeat();
        }

        private void UpdateGameObjects()
        { 
            List<GameObject> createdObjects = new List<GameObject>();

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(GameObjects, out List<GameObject> createdObjectsByThisGameObject);
                createdObjects.AddRange(createdObjectsByThisGameObject);
            }

            GameObjects.RemoveAll(gameObject => gameObject.ObjectType != ObjectType.PlayerShip &&
                                                (gameObject.PositionX + gameObject.Size / 2 < 0 ||
                                                 gameObject.PositionX > gameFieldWidth));

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject gameObject = GameObjects[i];

                if (gameObject.PositionY + gameObject.Size / 2 >= ground.PositionY + 5 &&
                    CanIntersect(gameObject, ground))
                {
                    if (gameObject.ObjectType == ObjectType.PlayerShip)
                        Defeat();

                    commandManager.ExecuteCommand(new CommandDeath(this, gameObject));
                    commandManager.ExecuteCommand(new CommandCreate(this, new Blast(new Point(gameObject.PositionX,
                        gameObject.PositionY + gameObject.Size / 2))));
                    continue;
                }

                for (int j = i; j < GameObjects.Count; j++)
                {
                    GameObject nextGameObject = GameObjects[j];

                    if (CanIntersect(gameObject, nextGameObject) &&
                        gameObject.IntersectsWith(nextGameObject) &&
                        nextGameObject.Health > 0)
                    {
                        int amountOfDamage = Math.Min(gameObject.Health, nextGameObject.Health);

                        commandManager.ExecuteCommand(new CommandTakeDamage(gameObject, amountOfDamage));
                        commandManager.ExecuteCommand(new CommandTakeDamage(nextGameObject, amountOfDamage));

                        if (gameObject.Health <= 0 || nextGameObject.Health <= 0)
                            commandManager.ExecuteCommand(new CommandCreate(this,
                                new Blast(gameObject.GetMiddleOfVector(nextGameObject))));

                        switch (gameObject.ObjectType)
                        {
                            case ObjectType.PlayerShip:
                            case ObjectType.PlayerBullet:
                                if (nextGameObject.Health <= 0 &&
                                    (nextGameObject.ObjectType == ObjectType.BomberShip ||
                                     nextGameObject.ObjectType == ObjectType.ChaserShip))
                                    commandManager.ExecuteCommand(new CommandChangeScore(this));
                                break;
                            case ObjectType.ChaserShip:
                            case ObjectType.BomberShip:
                                if (gameObject.Health <= 0 &&
                                    nextGameObject.ObjectType == ObjectType.PlayerBullet)
                                    commandManager.ExecuteCommand(new CommandChangeScore(this));
                                break;
                        }
                    }
                }
            }

            for (var i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].Health <= 0 && GameObjects[i].ObjectType != ObjectType.PlayerShip)
                    commandManager.ExecuteCommand(new CommandDeath(this, GameObjects[i]));
            }

            foreach (GameObject createdObject in createdObjects)
                commandManager.ExecuteCommand(new CommandCreate(this, createdObject));
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
                        commandManager.ExecuteCommand(new CommandCreate(this,
                            new ChaserShip(gameFieldWidth, Random.Next(100, gameFieldHeight - 350))));
                        break;
                    case 2:
                        commandManager.ExecuteCommand(new CommandCreate(this,
                            new BomberShip(gameFieldWidth, Random.Next(100, gameFieldHeight - 350))));
                        break;
                    case 3:
                        commandManager.ExecuteCommand(new CommandCreate(this,
                            new Meteor(Random.Next(gameFieldWidth - 100, gameFieldWidth), 0)));
                        break;
                    case 4:
                        commandManager.ExecuteCommand(new CommandCreate(this,
                            new Bird(gameFieldWidth, Random.Next(gameFieldHeight - 300, gameFieldHeight - 50))));
                        break;
                }
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawInterface(graphics);
            DrawGameObjects(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.LightCyan, 0, 0, gameFieldWidth, gameFieldHeight);
            ground.Draw(graphics);
        }

        private void DrawInterface(Graphics graphics)
        {
            graphics.DrawString("Score: " + Score, font, Brushes.Black, gameFieldWidth - 165, 10);
            graphics.DrawString("Player's health: " + PlayerShip.Health, font, Brushes.Black, gameFieldWidth - 165, 30);
        }

        private void DrawGameObjects(Graphics graphics)
        {
            foreach (GameObject gameObject in GameObjects)
               gameObject.Draw(graphics);
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

            [ObjectType.Bird] = ObjectType.PlayerShip,

            [ObjectType.Blast] = 0
        };

        private bool CanIntersect(GameObject firstGameObject, GameObject secondGameObject)
        {
            return intersectTable[firstGameObject.ObjectType].HasFlag(secondGameObject.ObjectType);
        }
    }
}
