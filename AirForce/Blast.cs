using System.Drawing;

namespace AirForce
{
    internal class Blast
    {
        private readonly int positionX;
        private readonly int positionY;
        private readonly int size;
        private static readonly Bitmap[] Bitmaps = {Crop(0), Crop(1), Crop(2), Crop(3)};

        public int StageOfBlast;

        public Blast(Point position)
        {
            positionX = position.X;
            positionY = position.Y;
            size = 20;
            StageOfBlast = 0;
        }

        public void SetNextStage()
        {
            StageOfBlast++;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(Bitmaps[StageOfBlast], positionX - 5, positionY - 5, size, size);
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
