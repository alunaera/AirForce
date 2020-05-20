using System.Drawing;

namespace AirForce
{
    internal class Blast : GameObject
    {
        private static readonly Bitmap[] Bitmaps = {Crop(0), Crop(1), Crop(2), Crop(3)};

        public Blast(Point position)
        {
            ObjectType = ObjectType.Blast;
            PositionX = position.X;
            PositionY = position.Y;
            Size = 20;
            Health = 4;
        }

        public override void Update(Game game)
        {
            Health--;
        }

        public override void Draw(Graphics graphics)
        {
            if (Health > 0)
                graphics.DrawImage(Bitmaps[4 - Health], PositionX - 5, PositionY - 5, Size, Size);
        }

        private static Bitmap Crop(int sectorNumber)
        {
            Bitmap bitmap = Properties.Resources.Blast;
            Rectangle selection = new Rectangle(sectorNumber * bitmap.Width / 4, 0, bitmap.Width / 4,
                bitmap.Height);

            return bitmap.Clone(selection, bitmap.PixelFormat);
        }
    }
}
