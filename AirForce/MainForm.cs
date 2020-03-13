﻿using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForce
{
    public partial class MainForm : Form
    {
        private readonly Game game = new Game();

        public MainForm()
        {
            InitializeComponent();

            gameField.Paint += Draw;

            game.Defeat += () =>
            {
                Timer.Enabled = false;
                MessageBox.Show("Game over");
                game.StartGame(1535, 900);
                Timer.Enabled = true;
            };

            game.StartGame(1535, 900);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            game.Draw(e.Graphics);
        }

        private void TickTimer(object sender, System.EventArgs e)
        {
            game.Update();
            gameField.Refresh();
        }

        private void DownKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.D:
                case Keys.S:
                case Keys.A:
                    game.ChangePlayerShipMoveMode(e.KeyCode);
                    break;
                case Keys.Space:
                   // game.MakeShot();
                    break;
            }
        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            game.SetPlayerShipMoveModeDefaultValue();
            gameField.Refresh();
        }
    }
}
