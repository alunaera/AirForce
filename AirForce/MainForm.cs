using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AirForce.States;

namespace AirForce
{
    public partial class MainForm : Form
    {
        private readonly Game game = new Game();

        public MainForm()
        {
            InitializeComponent();

            gameField.Paint += Draw;

            game.StartGame(ClientRectangle.Width, ClientRectangle.Height);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            game.Draw(e.Graphics);
        }

        private void TickTimer(object sender, System.EventArgs e)
        {
            game.TickTimer();
            gameField.Refresh();
        }

        private void DownKey(object sender, KeyEventArgs e)
        {
          
            game.DownKey(e.KeyCode);
        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            game.UpKey(e.KeyCode);
        }

        private void ClickHelp(object sender, System.EventArgs e)
        {
            MessageBox.Show("Move player's ship - WASD" +
                            "\nStart shooting - Space" +
                            "\nReverse - Shift ");
        }

        private void ClickPause(object sender, System.EventArgs e)
        {
            Timer.Enabled = !Timer.Enabled;
            pauseToolStripMenuItem.Text = Timer.Enabled ? "Pause" : "Continue";
        }
    }
}
