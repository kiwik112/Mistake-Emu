

using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Emil
{
    public class Key
    {
        static int[] numericKeys = Enumerable.Range(0x30, 0x39).ToArray();
        static int[] standardKeys = Enumerable.Range(0x41, 0x5A).Concat(numericKeys).ToArray();
        public static void handle(KeyEventArgs e)
        {
            switch (e.Modifiers)
            {
                case Keys.None:
                    if (standardKeys.Contains(e.KeyValue))
                    {
                        Screen.Write((char)e.KeyValue);
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
                                Screen.Write(' ');
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
                            case var expression when (e.KeyCode >= Keys.Oemcomma && e.KeyCode <= Keys.OemQuestion):
                                Screen.Write((char)(e.KeyValue - 144));
                                break;
                            case var _ when (e.KeyCode >= Keys.OemOpenBrackets && e.KeyCode <= Keys.OemCloseBrackets):
                                Screen.Write((char)(e.KeyValue - 128));
                                break;
                            case Keys.OemSemicolon:
                                Screen.Write(';');
                                break;
                            case Keys.Oemplus:
                                Screen.Write('=');
                                break;
                            case Keys.OemQuotes:
                                Screen.Write('\'');
                                break;
                            case Keys.Enter:
                                Kernel.NewLn();
                                Basic.Exec();
                                Kernel.NewLn();
                                break;
                        }
                    }
                    break;
                case Keys.Shift:
                    if (e.KeyValue >= 0x31 && e.KeyValue < 0x36) Screen.Write((char)(e.KeyValue - 16));
                    else if (e.KeyValue > 0x36 && e.KeyValue < 0x41 && e.KeyValue != 56) Screen.Write((char)(e.KeyValue - 17));
                    else switch (e.KeyCode)
                    {
                            case Keys.D0:
                                Screen.Write(')');
                                break;
                            case Keys.OemQuotes:
                                Screen.Write('"');
                                break;
                            case Keys.OemQuestion:
                                Screen.Write('?');
                                break;
                    }
                    break;
            }
        }
    }
}