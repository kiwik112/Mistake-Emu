using System.Diagnostics;

namespace Emil
{
    public partial class Form1 : Form
    {
        private int _cursorPos = 0;
        public int cursorPos
        {
            get
            {
                return _cursorPos;
            }
            set
            {
                int tempPos = _cursorPos;
                _cursorPos = value;
                CursorChangePos(tempPos);
            }
        }
        public static int charSet = 0xE000;
        public char[] screenRam = Enumerable.Repeat<char>((char)(' ' + charSet), 1000).ToArray();
        public Form1()
        {
            InitializeComponent();
        }
//CURSOR BLINK
        private void CursorBlink()
        {
            while (true)
            {
                Debug.WriteLine(charSet.ToString("X4") + " U+" + ((int)screenRam[cursorPos]).ToString("X4"));
                Thread.Sleep(500);
                if ((int)screenRam[cursorPos] < (0x0200 + charSet)) screenRam[cursorPos] += (char)0x0200;
                Debug.WriteLine(charSet.ToString("X4") + " U+" + ((int)screenRam[cursorPos]).ToString("X4"));
                Thread.Sleep(500);
                if ((int)screenRam[cursorPos] >= (0x0200 + charSet)) screenRam[cursorPos] -= (char)0x0200;
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
            screenRam = Key.Handle(e, screenRam, cursorPos, out int newPos);
            cursorPos = newPos;
        }
//PREVENT FROM LEAVING CURSOR BEHIND
        private void CursorChangePos(int pos)
        {
            if ((int)screenRam[cursorPos] < 0xE200) screenRam[cursorPos] += (char)0x0200;
            if ((int)screenRam[pos] >= 0xE200) screenRam[pos] -= (char)0x0200;
        }
//SCREEN UPDATE
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMain.Text = new string(screenRam);
        }
    }
}