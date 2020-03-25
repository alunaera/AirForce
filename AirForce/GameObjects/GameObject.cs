using System;
using System.Collections.Generic;
using System.Drawing;

namespace AirForce
{
    internal abstract class GameObject
    {
        protected Bitmap Bitmap;

        public ObjectType ObjectType { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Health { get; protected set; }
        public int Size { get; protected set; }

        public abstract void Update(List<GameObject> gameObjects, out List<GameObject> createdObjects);

        public void Destroy()
        {
            Health = 0;
        }

        public void TakeDamage(int amountOfDamage)
        {
            Health -= amountOfDamage;
        }

        public bool IntersectsWith(GameObject gameObject)
        {
            return GetDistanceToObject(gameObject.PositionX, gameObject.PositionY) <= (Size + gameObject.Size) / 2;
        }

        public Point GetMiddleOfVector(GameObject gameObject)
        {
            int positionX = (PositionX + gameObject.PositionX) / 2;
            int positionY = (PositionY + gameObject.PositionY) / 2;

            return new Point(positionX, positionY);
        }

        protected int GetDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return (int) Math.Sqrt(componentX + componentY);
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawImage(Bitmap, PositionX - Size / 2, PositionY - Size / 2, Size, Size);
        }
    }
}