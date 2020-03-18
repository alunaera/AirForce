using System;
using System.Collections.Generic;
using System.Drawing;

namespace AirForce
{
    internal abstract class GameObject
    {
        public ObjectType ObjectType { get; protected set; }
        public Bitmap Bitmap { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Health { get; protected set; }
        public int Size { get; protected set; }

        public abstract void Move(List<GameObject> gameObjects, out List<GameObject> createdObjects);

        public void Destroy()
        {
            Health = 0;
        }

        public void TakeDamage(int gameObjectHealth)
        {
            Health -= Math.Min(Health, gameObjectHealth);
        }

        public bool IsIntersection(GameObject gameObject)
        {
            return GetDistanceToObject(gameObject.PositionX, gameObject.PositionY) <= (Size + gameObject.Size) / 2;
        }

        protected int GetDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return (int) Math.Sqrt(componentX + componentY);
        }
    }
}