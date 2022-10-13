

using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Emil
{
    public class Key
    {
        public static void handle(KeyEventArgs e)
        {
            switch (e.Modifiers)
            {
                case Keys.None:
                    if ((e.KeyValue >= 0x30 && e.KeyValue <= 0x39) || (e.KeyValue >= 0x41 && e.KeyValue <= 0x5A))
                    {
                        Screen.ram[Screen.cursorPos] = (char)e.KeyValue;
                        Screen.cursorPos++;
                    }
                    else
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.Back:
                                if (Screen.cursorPos > 0)
                                {
                                    Screen.cursorPos--;
                                    Screen.ram[Screen.cursorPos] = ' ';
                                }
                                break;
                            case Keys.Space:
                                if (Screen.cursorPos < 999) Screen.cursorPos++;
                                break;
                            case Keys.Up:
                                if (Screen.cursorPos >= 40) Screen.cursorPos -= 40;
                                break;
                            case Keys.Down:
                                if (Screen.cursorPos <= 999 - 40) Screen.cursorPos += 40;
                                break;
                            case Keys.Right:
                                if (Screen.cursorPos < 999) Screen.cursorPos++;
                                break;
                            case Keys.Left:
                                if (Screen.cursorPos > 0) Screen.cursorPos--;
                                break;
                        }
                    }
                    break;
            }
        }
    }
}