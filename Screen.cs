using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Emil
{
    public class Screen
    {
        private static int _pos = 0;
        private static bool cursor = false;
        public static int cursorPos
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
                cursor = true;
            }
        }
        public static void CursorBlink()
        {
            while (true)
            {
                Thread.Sleep(800);
                cursor = !cursor;
            }
        }

        public static int charSet = 0xE000;
        private static char[] chars = new char[1000];
        public static char[] ram = Enumerable.Repeat<char>((char)(' '), 1000).ToArray();
        public static string update()
        {
            for(int i = 0; i < 1000; i++)
            {
                chars[i] = (char)(ram[i] + charSet);
            }
            if (cursor) chars[cursorPos] += (char)0x0200;
            return new string(chars);
        }
        public static void Write(char character)
        {
            if (cursorPos >= 999) return;
            Screen.ram[Screen.cursorPos] = character;
            Screen.cursorPos++;
        }
    }
}
