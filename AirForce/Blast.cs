using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    internal class Blast
    {
        private readonly int positionX;
        private readonly int positionY;
        private readonly int size;
        private readonly Bitmap bitmap;

        public int StageOfBlast;

        public Blast(Point position)
        {
            positionX = position.X;
            positionY = position.Y;
            size = 20;
            bitmap = Properties.Resources.Blast;
            StageOfBlast = 1;
        }

        public void SetNextStage()
        {
            StageOfBlast++;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(Crop(new Rectangle((StageOfBlast - 1) * bitmap.Width / 4, 0, bitmap.Width / 4, bitmap.Height)),
                positionX - 5, positionY - 5, size, size);
        }

        private Bitmap Crop(Rectangle selection)
        {
            return bitmap.Clone(selection, bitmap.PixelFormat);
        }
    }
}
