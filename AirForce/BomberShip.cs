namespace AirForce
{
    class BomberShip : ObjectOnGameField
    {
        public BomberShip(int positionX, int positionY)
        {
            TypeOfObject = TypeOfObject.BomberShip;
            PositionX = positionX;
            PositionY = positionY;
            Health = 3;
            Size = 80;
        }
    }
}
