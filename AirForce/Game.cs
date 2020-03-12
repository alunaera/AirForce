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
        private readonly Font font = new Font("Arial", 10);

        private int gameFieldWidth;
        private int gameFieldHeight;
        private PlayerShip PlayerShip => (PlayerShip)objectsOnGameFieldList[0];
        private Ground ground;
        private List<ObjectOnGameField> objectsOnGameFieldList;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;

            ground = new Ground();
            objectsOnGameFieldList = new List<ObjectOnGameField> {new PlayerShip()};
        }

        public void Update()
        {
            MoveObjectsOnGameField();

            if (IsDefeat())
                Defeat();

            if (objectsOnGameFieldList.Count < 2)
                GenerateEnemies();
        }

        private void MoveObjectsOnGameField()
        {
            foreach (ObjectOnGameField objectOnGameField in objectsOnGameFieldList)
                objectOnGameField.Move();

            for (int i = 0; i < objectsOnGameFieldList.Count; i++)
            {
                ObjectOnGameField objectOnGameField = objectsOnGameFieldList[i];

                if (objectOnGameField.PositionX + objectOnGameField.Size < 0)
                    objectOnGameField.Destroy();

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
                    }
                }
                else
                {
                    objectsOnGameFieldList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ChangePlayerShipMoveMode(Keys keyCode)
        {
            PlayerShip.ChangeMoveMode(keyCode);
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
                        objectsOnGameFieldList.Add(new Bird(1500, Random.Next(100, 500)));
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
            DrawObjectsOnGameField(graphics);

            graphics.DrawString(PlayerShip.Health.ToString(), font, Brushes.Black, 1400, 10);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.LightBlue, 0, 0, gameFieldWidth, gameFieldHeight);
            graphics.DrawImage(ground.Bitmap,
                ground.PositionX, ground.PositionY,
                ground.Width, ground.Height);
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
                        ObjectType.Meteor,
                        ObjectType.Bird,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.ChaserShip, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.BomberShip,
                        ObjectType.Meteor,
                        ObjectType.Ground
                    }
                },

                {
                    ObjectType.BomberShip, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.ChaserShip,
                        ObjectType.Meteor,
                        ObjectType.Ground
                    }
                },
                {
                    ObjectType.Meteor, new HashSet<ObjectType>()
                    {
                        ObjectType.PlayerShip,
                        ObjectType.ChaserShip,
                        ObjectType.BomberShip,
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
