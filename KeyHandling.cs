

using System.Diagnostics.Tracing;
using System.Windows.Forms;

namespace Emil
{
    public class Key
    {
        public static char[] Handle(KeyEventArgs e, char[] screenRam, int cursorPos, out int outPos)
        {
//CHECK MOD
            switch (e.Modifiers)
            {
                case Keys.None:
                    screenRam = None(e, screenRam, cursorPos, out cursorPos);
                    break;
                case Keys.Shift:
                    screenRam = Shift(e, screenRam, cursorPos, out cursorPos);
                    break;
                case Keys.Control:
                    screenRam = Ctrl(e, screenRam, cursorPos, out cursorPos);
                    break;
                case Keys.Alt:
                    screenRam = Alt(e, screenRam, cursorPos, out cursorPos);
                    break;
            }
            outPos = cursorPos;
            return screenRam;
        }
//NO MOD
        static char[] None(KeyEventArgs key, char[] screenRam, int cursorPos, out int outPos)
        {
    //ALPHANUM KEYS
            if ((key.KeyCode >= Keys.A && key.KeyCode <= Keys.Z) || (key.KeyCode >= Keys.D0 && key.KeyCode <= Keys.D9))
            {
                screenRam[cursorPos] = Convert.ToChar(key.KeyCode + Form1.charSet);
                cursorPos++;
            }
            switch (key.KeyCode)
            {
    //BACKSPACE
                case Keys.Back:
                    if (cursorPos == 0) break;
                    cursorPos--;
                    screenRam[cursorPos] = (char)(' ' + Form1.charSet + 0x0200);
                    break;
    //SPACE
                case Keys.Space:
                    screenRam[cursorPos] = (char)(' ' + Form1.charSet + 0x0200);
                    cursorPos++;
                    break;
    //LEFT
                case Keys.Left:
                    if (cursorPos > 0) cursorPos--;
                    break;
    //RIGHT
                case Keys.Right:
                    if (cursorPos < 999) cursorPos++;
                    break;
    //UP
                case Keys.Up:
                    if (cursorPos < 40) break;
                    cursorPos -= 40;
                    break;
    //DOWN
                case Keys.Down:
                    if (cursorPos > 960) break;
                    cursorPos += 40;
                    break;
            }
            outPos = cursorPos;
            return screenRam;
        }
//SHIFT
        static char[] Shift(KeyEventArgs key, char[] screenRam, int cursorPos, out int outPos)
        {
            switch (key.KeyCode)
            {
                
            }
            outPos = cursorPos;
            return screenRam;
        }
//CTRL
        static char[] Ctrl(KeyEventArgs key, char[] screenRam, int cursorPos, out int outPos)
        {
            switch (key.KeyCode)
            {
                case Keys.C:
                    if (Form1.charSet == 0xE000)
                    {
                        Form1.charSet = 0xE100;
                        for (int i = 0; i < 1000; i++)
                        {
                            screenRam[i] = (char)(screenRam[i] + 0x0100);
                        }
                    }
                    else
                    {
                        Form1.charSet = 0xE000;
                        for (int i = 0; i < 1000; i++)
                        {
                            screenRam[i] = (char)(screenRam[i] - 0x0100);
                        }
                    }
                    break;
                case Keys.Q:
                    Application.Exit();
                    break;
                case Keys.R:
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    Application.Exit();
                    break;
            }
            outPos = cursorPos;
            return screenRam;
        }
//ALT
        static char[] Alt(KeyEventArgs key, char[] screenRam, int cursorPos, out int outPos)
        {
            switch (key.KeyCode)
            {
                
            }
            outPos = cursorPos;
            return screenRam;
        }
    }
}