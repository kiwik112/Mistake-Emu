using System.Diagnostics;

namespace Emil
{
    public class Kernel
    {
        public static void Start()
        {
            Print("      * * *  PAINFUL BASIC  * * *\r     BASICALLY INFINITE BYTES FREE\r\r");
            Print("READY.\r");
        }
        public static void Print(string text)
        {
            foreach (char c in text)
            {
                switch (c)
                {
                    case '\r':
                        NewLn();
                        break;
                    default:
                        Screen.Write(c);
                        break;
                }
            }
        }
        public static void NewLn()
        {
            if (Screen.cursorPos >= 1000 - 40)
            {
                for (int i = 0; i < 1000 - 40; i++)
                {
                    Screen.ram[i] = Screen.ram[i + 40];
                }
                for (int i = 0; i < 40; i++)
                {
                    Screen.ram[1000 - 40 + i] = ' ';
                }
                Screen.cursorPos -= 40;
            }
            int row = Screen.cursorPos % 40;
            Screen.cursorPos += 40 - row;
        }
    }
}