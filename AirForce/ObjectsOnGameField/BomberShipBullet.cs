namespace AirForce
{
    internal class BomberShipBullet : ObjectOnGameField
    {
        public BomberShipBullet(int positionX, int positionY)
        {
            ObjectType = ObjectType.PlayerBullet;
            Bitmap = Properties.Resources.BomberShipBullet;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1;
            Size = 14;
        }

        public override void Move()
        {
            PositionX -= 7;
        }
    }
}
