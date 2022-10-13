using System.Diagnostics;

namespace Emil
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
//CURSOR BLINK
        private void CursorBlink()
        {
            while (true)
            {
                Thread.Sleep(500);
                if (Vars.screenRam[Vars.cursorPos] < (0x0200 + Vars.charSet)) Vars.screenRam[Vars.cursorPos] += (char)0x0200;
                Thread.Sleep(500);
                if (Vars.screenRam[Vars.cursorPos] >= (0x0200 + Vars.charSet)) Vars.screenRam[Vars.cursorPos] -= (char)0x0200;
            }
        }
//START THREADS
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread blinkThr = new Thread(CursorBlink);
            blinkThr.Start();
        }
//EXIT
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
//HANDLE KEYS
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Key.Handle(e);
        }
//PREVENT FROM LEAVING CURSOR BEHIND
        public static void CursorChangePos(int pos)
        {
            if ((int)Vars.screenRam[Vars.cursorPos] < 0xE200) Vars.screenRam[Vars.cursorPos] += (char)0x0200;
            if ((int)Vars.screenRam[pos] >= 0xE200) Vars.screenRam[pos] -= (char)0x0200;
        }
//SCREEN UPDATE
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMain.Text = new string(Vars.screenRam);
        }
        public int getline
        {
            get
            {
                return (Vars.cursorPos - Vars.cursorPos % 40) / 40;
            }
        }
    }
}