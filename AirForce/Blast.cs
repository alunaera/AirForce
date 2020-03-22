using System.Drawing;

namespace AirForce
{
    internal class Blast
    {
        private readonly int positionX;
        private readonly int positionY;
        private readonly int size;
        private readonly Bitmap[] bitmaps = { Crop(1), Crop(2), Crop(3), Crop(4) };

        public int StageOfBlast;

        public Blast(Point position)
        {
            positionX = position.X;
            positionY = position.Y;
            size = 20;
            StageOfBlast = 1;
        }

        public void SetNextStage()
        {
            StageOfBlast++;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(bitmaps[StageOfBlast - 1], positionX - 5, positionY - 5, size, size);
        }

        private static Bitmap Crop(int sectorNumber)
        {
            Bitmap bitmap = Properties.Resources.Blast;
            Rectangle selection = new Rectangle((sectorNumber - 1) * bitmap.Width / 4, 0, bitmap.Width / 4,
                bitmap.Height);

            return bitmap.Clone(selection, bitmap.PixelFormat);
        }
    }
}
