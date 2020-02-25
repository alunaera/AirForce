using System;
using System.Windows.Forms;

namespace AirForce
{
    class PlayerShip : Ship
    {
        public PlayerShip()
        {
            PositionX = 100;
            PositionY = 200;
        }

        public void Move(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.S:
                    this.PositionY += 5;
                    break;
                case Keys.W:
                    this.PositionY -= 5;
                    break;
                case Keys.A:
                    this.PositionX -= 5;
                    break;
                case Keys.D:
                    this.PositionX += 5;
                    break;
            }
        }
    }
}
