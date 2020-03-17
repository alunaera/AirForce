using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce
{
    internal class Game
    {
        private static readonly Random Random = new Random();
        private readonly Font font = new Font("Arial", 15);

        private int gameFieldWidth;
        private int gameFieldHeight;
        private int score;
        private PlayerShip PlayerShip => (PlayerShip)objectsOnGameFieldList[0];
        private Ground ground;
        private List<ObjectOnGameField> objectsOnGameFieldList;
        private List<ObjectOnGameField> createdObjectsList;
        private List<Point> blastList;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;
            score = 0;

            ground = new Ground();
            objectsOnGameFieldList = new List<ObjectOnGameField> {new PlayerShip()};
            blastList = new List<Point>();
        }

        public void Update()
        {
            if (IsDefeat())
                Defeat();

            blastList.Clear();

            MoveObjectsOnGameField();

            if (objectsOnGameFieldList.Count(objectOnGameField => objectOnGameField.ObjectType != ObjectType.PlayerBullet) < 2)
                GenerateEnemies();

            AddCreatedObjects();
        }

        private void MoveObjectsOnGameField()
        {
            foreach (ObjectOnGameField objectOnGameField in objectsOnGameFieldList)
                objectOnGameField.Move(objectsOnGameFieldList, out createdObjectsList);

            for (int i = 0; i < objectsOnGameFieldList.Count; i++)
            {
                ObjectOnGameField objectOnGameField = objectsOnGameFieldList[i];

                if (objectOnGameField.ObjectType != ObjectType.PlayerBullet)
                {
                    if (objectOnGameField.PositionX + objectOnGameField.Size < 0)
                        objectOnGameField.Destroy();
                }
                else
                {
                    if (objectOnGameField.PositionX + objectOnGameField.Size > gameFieldWidth)
                        objectOnGameField.Destroy();
                }

                if (objectOnGameField.PositionY + objectOnGameField.Size / 2 >= ground.PositionY + 5)
                    objectOnGameField.TakeDamage(ground.Health);

                if (objectOnGameField.Health > 0)
                {

                    var damageableObjectsList =
                        objectsOnGameFieldList.Where(nextObjectOnGameField =>
                            IsDamaged(objectOnGameField, nextObjectOnGameField));

                    foreach (ObjectOnGameField nextObjectOnGameField in damageableObjectsList)
                    {
                        int objectOnGameFieldHealth = objectOnGameField.Health;

                        objectOnGameField.TakeDamage(nextObjectOnGameField.Health);
                        nextObjectOnGameField.TakeDamage(objectOnGameFieldHealth);

                        if (objectOnGameField.ObjectType != ObjectType.PlayerBullet &&
                            objectOnGameField.ObjectType != ObjectType.PlayerShip ||
                            nextObjectOnGameField.ObjectType != ObjectType.BomberShip &&
                            nextObjectOnGameField.ObjectType != ObjectType.ChaserShip ||
                            nextObjectOnGameField.Health >= 1)
                            continue;

                        score++;
                    }
                }
                else
                {
                    blastList.Add(new Point(objectOnGameField.PositionX, objectOnGameField.PositionY));

                    if (i == 0)
                        continue;

                    objectsOnGameFieldList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void AddCreatedObjects()
        {
            foreach (ObjectOnGameField createdObject in createdObjectsList)
            {
                objectsOnGameFieldList.Add(createdObject);
            }
            createdObjectsList.Clear();
        }

        public void ChangePlayerShipMoveMode(Keys keyCode)
        {
            PlayerShip.ChangeMoveMode(keyCode);
        }

        public void MakeShot()
        {
            if (PlayerShip.DelayOfShot > 0) 
                return;

            objectsOnGameFieldList.Insert(1, new PlayerBullet(PlayerShip.PositionX + PlayerShip.Size / 2,
                PlayerShip.PositionY));
            PlayerShip.SetDelayOfShotDefaultValue();
        }

        public void SetPlayerShipMoveModeDefaultValue()
        {
            PlayerShip.SetMoveModeDefaultValue();
        }

        private void GenerateEnemies()
        {
            int generatedObjectsCount = Random.Next(1, 3);

            for (int i = 0; i < generatedObjectsCount; i++)
                switch (Random.Next(1, 5))
                {
                    case 1:
                        objectsOnGameFieldList.Add(new ChaserShip(1500, Random.Next(100, 500)));
                        break;
                    case 2:
                        objectsOnGameFieldList.Add(new BomberShip(1500, Random.Next(100, 500)));
                        break;
                    case 3:
                        objectsOnGameFieldList.Add(new Meteor(Random.Next(1400, 1500), 0));
                        break;
                    case 4:
                        objectsOnGameFieldList.Add(new Bird(1500, Random.Next(500, 700)));
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
            if(blastList.Count > 0)
                foreach (var blast in blastList)
                {
                    graphics.DrawImage(Properties.Resources.Blast, blast.X - 5, blast.Y - 5, 10, 10);
                }

        }

        private void DrawObjectsOnGameField(Graphics graphics)
        {
            graphics.DrawImage(PlayerShip.Bitmap,
                PlayerShip.PositionX - PlayerShip.Size / 2, PlayerShip.PositionY - PlayerShip.Size / 2,
                PlayerShip.Size, PlayerShip.Size);

            foreach (ObjectOnGameField objectOnGameField in objectsOnGameFieldList)
                graphics.DrawImage(objectOnGameField.Bitmap,
                    objectOnGameField.PositionX - objectOnGameField.Size / 2,
                    objectOnGameField.PositionY - objectOnGameField.Size / 2,
                    objectOnGameField.Size, objectOnGameField.Size);
        }

        private readonly Dictionary<ObjectType, HashSet<ObjectType>> intersectTable = new Dictionary<ObjectType, HashSet<ObjectType>>()

            {
                {
                    ObjectType.PlayerShip, new HashSet<ObjectType>()
                    {
                        ObjectType.ChaserShip,
                        ObjectType.BomberShip,
                        ObjectType.BomberShipBullet,
                        ObjectType.Meteor,
                        ObjectType.Bird,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.PlayerBullet, new HashSet<ObjectType>()
                    {
                        ObjectType.ChaserShip,
                        ObjectType.BomberShip,
                        ObjectType.Meteor,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.ChaserShip, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.PlayerBullet,
                        ObjectType.Meteor,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.BomberShip, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.PlayerBullet,
                        ObjectType.Meteor,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.BomberShipBullet, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.Meteor,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.Meteor, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.PlayerBullet,
                        ObjectType.ChaserShip,
                        ObjectType.BomberShip,
                        ObjectType.BomberShipBullet,
                        ObjectType.Ground
                    }
                },
                {
                    ObjectType.Bird, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip
                    }
                }
            };

        private bool IsDamaged(ObjectOnGameField firstObjectOnGame, ObjectOnGameField secondObjectOnGameField)
        {
            return intersectTable[firstObjectOnGame.ObjectType].Contains(secondObjectOnGameField.ObjectType) &&
                   firstObjectOnGame.IsIntersection(secondObjectOnGameField) &&
                   secondObjectOnGameField.Health > 0;
        }
    }
}
