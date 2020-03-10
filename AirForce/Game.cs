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

        private int gameFieldWidth;
        private int gameFieldHeight;
        private PlayerShip playerShip;
        private Ground ground;
        private List<ObjectOnGameField> objectsOnGameFieldList;

        public event Action Defeat = delegate { };

        public void StartGame(int gameFieldWidth, int gameFieldHeight)
        {
            this.gameFieldWidth = gameFieldWidth;
            this.gameFieldHeight = gameFieldHeight;

            playerShip = new PlayerShip();
            ground = new Ground();
            objectsOnGameFieldList = new List<ObjectOnGameField>();
        }

        public void Update()
        {
            MovePlayerShip();
            MoveObjectsOnGameField();

            if (IsDefeat())
                Defeat();

            if (objectsOnGameFieldList.Count == 0)
                GenerateEnemies();
        }

        private void MovePlayerShip()
        {
            playerShip.Move();

            if (playerShip.PositionY + playerShip.Size / 2 >= ground.PositionY + 5)
                playerShip.TakeDamage(ground);
        }

        private void MoveObjectsOnGameField()
        {
            for (int i = 0; i < objectsOnGameFieldList.Count; i++)
            {
                ObjectOnGameField objectOnGameField = objectsOnGameFieldList[i];

                if (objectOnGameField.Health > 0)
                {
                    objectOnGameField.Move();

                    if (objectOnGameField.PositionX + objectOnGameField.Size < 0)
                        objectOnGameField.Destroy();

                    if (objectOnGameField.IsIntersection(playerShip))
                    {
                        playerShip.TakeDamage(objectOnGameField);
                        objectOnGameField.TakeDamage(playerShip);
                    }

                    var damageableObjectList = objectsOnGameFieldList.Where(nextObjectOnGameField =>
                        objectOnGameField.IsIntersection(nextObjectOnGameField) &&
                        IsDamaged(objectOnGameField, nextObjectOnGameField));

                    foreach (ObjectOnGameField nextObjectOnGameField in damageableObjectList)
                        objectOnGameField.TakeDamage(nextObjectOnGameField);
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
            playerShip.ChangeMoveMode(keyCode);
        }

        public void SetPlayerShipMoveModeDefaultValue()
        {
            playerShip.SetMoveModeDefaultValue();
        }

        private void GenerateEnemies()
        {
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
            return playerShip.Health < 1;
        }

        public void Draw(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawObjectsOnGameField(graphics);
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
            graphics.DrawImage(playerShip.Bitmap,
                playerShip.PositionX - playerShip.Size / 2, playerShip.PositionY - playerShip.Size / 2,
                playerShip.Size, playerShip.Size);

            foreach (ObjectOnGameField objectOnGameField in objectsOnGameFieldList)
                graphics.DrawImage(objectOnGameField.Bitmap,
                    objectOnGameField.PositionX - objectOnGameField.Size / 2,
                    objectOnGameField.PositionY - objectOnGameField.Size / 2,
                    objectOnGameField.Size, objectOnGameField.Size);
        }

        private readonly Dictionary<ObjectType, Dictionary<ObjectType, bool>> intersectTable =
            new Dictionary<ObjectType, Dictionary<ObjectType, bool>>
            {
                {
                    ObjectType.PlayerShip, new Dictionary<ObjectType, bool>
                    {
                        [ObjectType.PlayerShip] = false,
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
                        [ObjectType.ChaserShip] = false,
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
                        [ObjectType.BomberShip] = false,
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
                        [ObjectType.Meteor] = false,
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
                        [ObjectType.Bird] = false,
                        [ObjectType.Ground] = false
                    }
                }
            };

        private bool IsDamaged(ObjectOnGameField firstObjectOnGame, ObjectOnGameField secondObjectOnGameField)
        {
            return intersectTable[firstObjectOnGame.ObjectType][secondObjectOnGameField.ObjectType];
        }
    }
}
