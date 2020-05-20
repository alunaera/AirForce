using System;
using System.Drawing;

namespace AirForce
{
    internal abstract class GameObject
    {
        protected Bitmap Bitmap;

        public ObjectType ObjectType { get; protected set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Health { get; set; }
        public int Size { get; protected set; }

        public abstract void Update(Game game);

        public bool IntersectsWith(GameObject gameObject)
        {
            return GetSqrDistanceToObject(gameObject.PositionX, gameObject.PositionY) <= (Size + gameObject.Size) * (Size + gameObject.Size) / 4;
        }

        public Point GetMiddleOfVector(GameObject gameObject)
        {
            int positionX = (PositionX + gameObject.PositionX) / 2;
            int positionY = (PositionY + gameObject.PositionY) / 2;

            return new Point(positionX, positionY);
        }

        protected double GetSqrDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return componentX + componentY;
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawImage(Bitmap, PositionX - Size / 2, PositionY - Size / 2, Size, Size);
        }
    }
}