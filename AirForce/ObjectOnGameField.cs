using System;


namespace AirForce
{
    internal abstract class ObjectOnGameField
    {
        public TypeOfObject TypeOfObject { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int Health { get; protected set; }
        public int Size { get; protected set; }

        public bool IsIntersection(int positionX, int positionY, int size)
        {
            return GetDistanceToObject(positionX, positionY) <= (Size + size) / 2;
        }

        private int GetDistanceToObject(int objectX, int objectY)
        {
            double componentX = Math.Pow(PositionX - objectX, 2);
            double componentY = Math.Pow(PositionY - objectY, 2);

            return (int)Math.Sqrt(componentX + componentY);
        }
    }
}
