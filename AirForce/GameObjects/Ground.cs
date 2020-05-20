using System.Drawing;

namespace AirForce
{
    internal class Ground : GameObject
    {
        private readonly int width;
        private readonly int height;

        public Ground(int positionX, int positionY)
        {
            width = 1550;
            height = 120;
            Bitmap = Properties.Resources.Ground;
            ObjectType = ObjectType.Ground;
            PositionX = positionX;
            PositionY = positionY;
            Health = 1000;
            Size = 0;
        }

        public override void Update(Game game)
        {
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(Bitmap, PositionX, PositionY, width, height);
        }
    }
}
