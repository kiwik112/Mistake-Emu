using System.Runtime.InteropServices;
using System.Security.Policy;

namespace Emil
{
    public class Vars
    {
        private static int _cursorPos = 0;
        public static int cursorPos
        {
            get
            {
                return _cursorPos;
            }
            set
            {
                int tempPos = _cursorPos;
                if (value >= 1000)
                {
                    for (int i = 0; i < 1000 - 40; i++)
                    {
                        screenRam[i] = screenRam[i + 40];
                    }
                    for (int i = 1000 - 40; i < 1000; i++)
                    {
                        screenRam[i] = ' ';
                    }
                    _cursorPos = value - 40;
                }
                else _cursorPos = value;
                Form1.CursorChangePos(tempPos);
            }
        }
        public static int charSet = 0xE000;
        public static char[] screenRam = Enumerable.Repeat<char>((char)(' ' + charSet), 1000).ToArray();
    }
}
