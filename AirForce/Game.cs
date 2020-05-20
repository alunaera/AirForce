using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AirForce.Commands;
using AirForce.States;

namespace AirForce
{
    internal class Game
    {
        public static readonly Random Random = new Random();
        public List<GameObject> GameObjects;
        public CommandManager CommandManager;
        public State CurrentState;
        public NormalState NormalState;
        public ReverseState ReverseState;
        public int Score;
        public int GameFieldWidth { get; private set; }
        public int GameFieldHeight { get; private set; }

        private readonly Font font = new Font("Arial", 15);

        private DefeatState defeatState;
        private PlayerShip PlayerShip => (PlayerShip)GameObjects[0];
        private Ground ground;


        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            GameFieldWidth = gameFieldWidth;
            GameFieldHeight = gameFieldHeight;
            Score = 0;

            ground = new Ground(0, gameFieldHeight - 120);
            GameObjects = new List<GameObject> {new PlayerShip(gameFieldWidth)};
            CommandManager = new CommandManager();
            NormalState = new NormalState(this);
            ReverseState = new ReverseState(this);
            defeatState = new DefeatState(this);
            CurrentState = NormalState;
        }

        public void TickTimer()
        {
            CurrentState.UpdateGame();
        }

        public void Update()
        {
            CommandManager.CreateNewRoster();
                UpdateGameObjects();

                if (GameObjects.Count(gameObject =>
                    gameObject.ObjectType != ObjectType.PlayerBullet &&
                    gameObject.ObjectType != ObjectType.BomberShipBullet &&
                    gameObject.ObjectType != ObjectType.Blast) < 2)
                    GenerateEnemies();

                if (PlayerShip.Health < 1)
                    CurrentState = defeatState;
        }

        private void UpdateGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
                GameObjects[i].Update(this);

            GameObjects.RemoveAll(gameObject => gameObject.ObjectType != ObjectType.PlayerShip &&
                                                (gameObject.PositionX + gameObject.Size / 2 < 0 ||
                                                 gameObject.PositionX > GameFieldWidth));

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject gameObject = GameObjects[i];

                if (gameObject.PositionY + gameObject.Size / 2 >= ground.PositionY + 5 &&
                    CanIntersect(gameObject, ground))
                {
                    if (gameObject.ObjectType == ObjectType.PlayerShip)
                        CurrentState = defeatState;
                    else
                    {
                        CommandManager.ExecuteCommand(new CommandDeath(this, gameObject));
                        CommandManager.ExecuteCommand(new CommandCreate(this, new Blast(new Point(gameObject.PositionX,
                            gameObject.PositionY + gameObject.Size / 2))));
                        continue;
                    }
                }

                for (int j = i; j < GameObjects.Count; j++)
                {
                    GameObject nextGameObject = GameObjects[j];

                    if (CanIntersect(gameObject, nextGameObject) &&
                        gameObject.IntersectsWith(nextGameObject) &&
                        nextGameObject.Health > 0)
                    {
                        int amountOfDamage = Math.Min(gameObject.Health, nextGameObject.Health);

                        CommandManager.ExecuteCommand(new CommandTakeDamage(gameObject, amountOfDamage));
                        CommandManager.ExecuteCommand(new CommandTakeDamage(nextGameObject, amountOfDamage));

                        if (gameObject.Health <= 0 || nextGameObject.Health <= 0)
                            CommandManager.ExecuteCommand(new CommandCreate(this,
                                new Blast(gameObject.GetMiddleOfVector(nextGameObject))));

                        switch (gameObject.ObjectType)
                        {
                            case ObjectType.PlayerShip:
                            case ObjectType.PlayerBullet:
                                if (nextGameObject.Health <= 0 &&
                                    (nextGameObject.ObjectType == ObjectType.BomberShip ||
                                     nextGameObject.ObjectType == ObjectType.ChaserShip))
                                    CommandManager.ExecuteCommand(new CommandChangeScore(this));
                                break;
                            case ObjectType.ChaserShip:
                            case ObjectType.BomberShip:
                                if (gameObject.Health <= 0 &&
                                    nextGameObject.ObjectType == ObjectType.PlayerBullet)
                                    CommandManager.ExecuteCommand(new CommandChangeScore(this));
                                break;
                        }
                    }
                }
            }

            for (int i = 0; i < GameObjects.Count; i++)
                if (GameObjects[i].Health <= 0 && GameObjects[i].ObjectType != ObjectType.PlayerShip)
                    CommandManager.ExecuteCommand(new CommandDeath(this, GameObjects[i]));
        }

        public void StartMovingPlayerShip(MoveMode moveMode)
        {
            PlayerShip.StartMoving(moveMode);
        }

        public void StopMovingPlayerShip(MoveMode moveMode)
        {
            PlayerShip.StopMoving(moveMode);
        }

        public void ClearPlayerShipsMoveMode()
        {
            PlayerShip.ClearMoveMode();
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
                        CommandManager.ExecuteCommand(new CommandCreate(this,
                            new ChaserShip(GameFieldWidth, Random.Next(100, GameFieldHeight - 350))));
                        break;
                    case 2:
                        CommandManager.ExecuteCommand(new CommandCreate(this,
                            new BomberShip(GameFieldWidth, Random.Next(100, GameFieldHeight - 350))));
                        break;
                    case 3:
                        CommandManager.ExecuteCommand(new CommandCreate(this,
                            new Meteor(Random.Next(GameFieldWidth - 100, GameFieldWidth), 0)));
                        break;
                    case 4:
                        CommandManager.ExecuteCommand(new CommandCreate(this,
                            new Bird(GameFieldWidth, Random.Next(GameFieldHeight - 300, GameFieldHeight - 50))));
                        break;
                }
        }

        public void DownKey(Keys keyCode)
        {
            CurrentState.DownKey(keyCode);
        }

        public void UpKey(Keys keyCode)
        {
            CurrentState.UpKey(keyCode);
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawInterface(graphics);
            DrawGameObjects(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.LightCyan, 0, 0, GameFieldWidth, GameFieldHeight);
            ground.Draw(graphics);
        }

        private void DrawInterface(Graphics graphics)
        {
            graphics.DrawString("Score: " + Score, font, Brushes.Black, GameFieldWidth - 165, 10);
            graphics.DrawString("Player's health: " + PlayerShip.Health, font, Brushes.Black, GameFieldWidth - 165, 30);

            if (CurrentState is DefeatState)
                graphics.DrawString("Press Shift to reverse time  \nPress R to start new game",
                    font, Brushes.Black, GameFieldWidth / 2 - 80, GameFieldHeight / 2);
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
