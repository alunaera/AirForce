using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AirForce
{
    public partial class MainForm : Form
    {
        private readonly Game game = new Game();
        private bool isPlayerShooting;

        public MainForm()
        {
            InitializeComponent();

            gameField.Paint += Draw;

            game.Defeat += () =>
            {
                Timer.Enabled = false;
                isPlayerShooting = false;
                MessageBox.Show("Game over");
                game.StartGame(ClientRectangle.Width, ClientRectangle.Height);
                Timer.Enabled = true;
            };

            game.StartGame(ClientRectangle.Width, ClientRectangle.Height);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            game.Draw(e.Graphics);
        }

        private void TickTimer(object sender, System.EventArgs e)
        {
            if (isPlayerShooting)
                game.StartShooting();

            game.Update();
            gameField.Refresh();
        }

        private void DownKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    game.StartMovingPlayerShip(MoveMode.Up);
                    break;
                case Keys.D:
                    game.StartMovingPlayerShip(MoveMode.Right);
                    break;
                case Keys.S:
                    game.StartMovingPlayerShip(MoveMode.Down);
                    break;
                case Keys.A:
                    game.StartMovingPlayerShip(MoveMode.Left);
                    break;
                case Keys.Space:
                    isPlayerShooting = true;
                    break;
            }
        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    game.StopMovingPlayerShip(MoveMode.Up);
                    break;
                case Keys.D:
                    game.StopMovingPlayerShip(MoveMode.Right);
                    break;
                case Keys.S:
                    game.StopMovingPlayerShip(MoveMode.Down);
                    break;
                case Keys.A:
                    game.StopMovingPlayerShip(MoveMode.Left);
                    break;
                case Keys.Space:
                    isPlayerShooting = false;
                    break;
            }
        }

        private void ClickHelp(object sender, System.EventArgs e)
        {
            MessageBox.Show("Move player's ship - WASD" +
                            "\nStart shooting - Space");
        }

        private void ClickPause(object sender, System.EventArgs e)
        {
            Timer.Enabled = !Timer.Enabled;
            pauseToolStripMenuItem.Text = Timer.Enabled ? "Pause" : "Continue";
        }
    }
}
