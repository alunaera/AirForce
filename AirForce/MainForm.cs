using System.Deployment.Application;
using System.Drawing.Drawing2D;
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
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            game.Draw(e.Graphics);
        }
    }
}
