using System;
using System.Drawing;

namespace AirForce
{
    internal abstract class ObjectOnGameField
    {
        public ObjectType ObjectType { get; protected set; }
        public Bitmap Bitmap { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Health { get; protected set; }
        public int Size { get; protected set; }

        public abstract void Move();

        public void Destroy()
        {
            Health = 0;
        }

        public void TakeDamage(int objectOnGameFieldHealth)
        {
            Health -= Math.Min(Health, objectOnGameFieldHealth);
        }

        public bool IsIntersection(ObjectOnGameField objectOnGameField)
        {
            return GetDistanceToObject(objectOnGameField.PositionX, objectOnGameField.PositionY) <= (Size + objectOnGameField.Size) / 2;
        }

        private int GetDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return (int) Math.Sqrt(componentX + componentY);
        }
    }
}