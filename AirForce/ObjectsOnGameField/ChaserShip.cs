namespace AirForce
{
    internal class ChaserShip : Ship
    {
        public ChaserShip(int positionX, int positionY)
        {
            ObjectType = ObjectType.ChaserShip;
            Bitmap = Properties.Resources.ChaserShip;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 80;
        }
    }
}
