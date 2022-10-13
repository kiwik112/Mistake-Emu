using System.Diagnostics;

namespace Emil
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread blinkThr = new Thread(Screen.CursorBlink);
            blinkThr.Start();
        }
//PASSING TO OTHER CLASSES
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMain.Text = Screen.update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Key.handle(e);
        }
    }
}