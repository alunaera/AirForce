namespace AirForce
{
    internal class BomberShip : Ship
    {
        public BomberShip(int positionX, int positionY)
        {
            ObjectType = ObjectType.BomberShip;
            Bitmap = Properties.Resources.BomberShip;
            PositionX = positionX;
            PositionY = positionY;
            Health = 3;
            Size = 80;
        }
    }
}
