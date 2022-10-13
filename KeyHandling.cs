

using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Emil
{
    public class Key
    {
        public static void Handle(KeyEventArgs e)
        {
//CHECK MOD
            switch (e.Modifiers)
            {
                case Keys.None:
                    None(e);
                    break;
                case Keys.Shift:
                    Shift(e);
                    break;
                case Keys.Control:
                    Ctrl(e);
                    break;
            }
        }
//NO MOD
        static void None(KeyEventArgs key)
        {
    //ALPHANUM KEYS
            if ((key.KeyCode >= Keys.A && key.KeyCode <= Keys.Z) || (key.KeyCode >= Keys.D0 && key.KeyCode <= Keys.D9))
            {
                Vars.screenRam[Vars.cursorPos] = Convert.ToChar(key.KeyCode + Vars.charSet);
                Vars.cursorPos++;
            }
            switch (key.KeyCode)
            {
    //BACKSPACE
                case Keys.Back:
                    if (Vars.cursorPos == 0) break;
                    Vars.cursorPos--;
                    Vars.screenRam[Vars.cursorPos] = (char)(' ' + Vars.charSet + 0x0200);
                    break;
    //SPACE
                case Keys.Space:
                    Vars.screenRam[Vars.cursorPos] = (char)(' ' + Vars.charSet + 0x0200);
                    Vars.cursorPos++;
                    break;
    //LEFT
                case Keys.Left:
                    if (Vars.cursorPos > 0) Vars.cursorPos--;
                    break;
    //RIGHT
                case Keys.Right:
                    if (Vars.cursorPos < 999) Vars.cursorPos++;
                    break;
    //UP
                case Keys.Up:
                    if (Vars.cursorPos < 40) break;
                    Vars.cursorPos -= 40;
                    break;
    //DOWN
                case Keys.Down:
                    if (Vars.cursorPos > 960) break;
                    Vars.cursorPos += 40;
                    break;
    //PERIOD
                case Keys.OemPeriod:
                    Vars.screenRam[Vars.cursorPos] = (char)('.' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //COMMA
                case Keys.Oemcomma:
                    Vars.screenRam[Vars.cursorPos] = (char)(',' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //QUOTES
                case Keys.OemQuotes:
                    Vars.screenRam[Vars.cursorPos] = (char)('\'' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //ENTER
                case Keys.Enter:
                    string temp = new string(Vars.screenRam, Vars.cursorPos - Vars.cursorPos % 40, 40);
                    char[] exec = new char[40];
                    for (int i = 0; i < 40; i++)
                    {
                        if (temp[i] > 0x0200 + Vars.charSet) exec[i] = (char)(temp[i] - 0x0200 - Vars.charSet);
                        else exec[i] = (char)(temp[i] - Vars.charSet);
                    }
                    Vars.cursorPos += 40 - (Vars.cursorPos % 40);
                    Basic.Do(new string(exec).Trim());
                    break;
            }
        }
//SHIFT
        static void Shift(KeyEventArgs key)
        {
            switch (key.KeyCode)
            {
    //EXCLAMATION
                case Keys.D1:
                    Vars.screenRam[Vars.cursorPos] = (char)('!' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //AT SIGN
                case Keys.D2:
                    Vars.screenRam[Vars.cursorPos] = (char)('@' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //HASH
                case Keys.D3:
                    Vars.screenRam[Vars.cursorPos] = (char)('#' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //DOLLAR SIGN
                case Keys.D4:
                    Vars.screenRam[Vars.cursorPos] = (char)('$' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //PERCENT SIGN
                case Keys.D5:
                    Vars.screenRam[Vars.cursorPos] = (char)('%' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //CARET
                case Keys.D6:
                    Vars.screenRam[Vars.cursorPos] = (char)('^' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //AND SIGN
                case Keys.D7:
                    Vars.screenRam[Vars.cursorPos] = (char)('&' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //ASTERISK
                case Keys.D8:
                    Vars.screenRam[Vars.cursorPos] = (char)('*' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //BRACKETS
                case Keys.D9:
                    Vars.screenRam[Vars.cursorPos] = (char)('(' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
                case Keys.D0:
                    Vars.screenRam[Vars.cursorPos] = (char)(')' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //GREATER THAN
                case Keys.OemPeriod:
                    Vars.screenRam[Vars.cursorPos] = (char)('>' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
    //LESS THAN
                case Keys.Oemcomma:
                    Vars.screenRam[Vars.cursorPos] = (char)('<' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
                case Keys.OemQuotes:
                    Vars.screenRam[Vars.cursorPos] = (char)('"' + Vars.charSet);
                    Vars.cursorPos++;
                    break;
            }
        }
//CTRL
        static void Ctrl(KeyEventArgs key)
        {
            switch (key.KeyCode)
            {
                case Keys.C:
                    if (Vars.charSet == 0xE000)
                    {
                        Vars.charSet = 0xE100;
                        for (int i = 0; i < 1000; i++)
                        {
                            Vars.screenRam[i] = (char)(Vars.screenRam[i] + 0x0100);
                        }
                    }
                    else
                    {
                        Vars.charSet = 0xE000;
                        for (int i = 0; i < 1000; i++)
                        {
                            Vars.screenRam[i] = (char)(Vars.screenRam[i] - 0x0100);
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
        }
    }
}