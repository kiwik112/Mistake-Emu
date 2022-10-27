using System.Diagnostics;

namespace Emil
{
    public class Basic
    {
        static Dictionary<int, string> lines = new Dictionary<int, string>();
        public static void Exec()
        {
            string line = "";
            for (int i = 0; i < 40; i++)
            {
                line += Screen.ram[i + (Screen.cursorPos - (Screen.cursorPos % 40)) - 40];
            }
            if (int.TryParse(line[0].ToString(), out int _)) Save(line.Trim());
            Execute(line, false);
        }
        private static void Execute(string line, bool programmed)
        {
            string[] parameters = line.Split(',');
            string cmd = line.Split(' ')[0];
            parameters[0] = parameters[0].Remove(0, cmd.Length);
            int len = parameters.Length;
            switch (cmd)
            {
                case "PRINT":
                    string[] text = line.Split('"');
                    if (text[0] == "PRINT " && text.Length == 3) Kernel.Print(text[1] + "\r");
                    break;
                case "CLEAR":
                    if (len == 1)
                    {
                        Screen.cursorPos = 0;
                        for (int i = 0; i < 1000; i++)
                        {
                            Screen.ram[i] = ' ';
                        }
                        break;
                    }
                    Kernel.Print("?SYNTAX ERROR");
                    break;
                case "EXIT":
                    if (len == 1) Environment.Exit(0);
                    else Kernel.Print("?SYNTAX ERROR");
                    break;
                case "VPOKE":
                    if (len == 2 && int.TryParse(parameters[0], out int address) && int.TryParse(parameters[1], out int value)) Screen.ram[address] = (char)value;
                    else Kernel.Print("?SYNTAX ERROR");
                    break;
                default:
                    Kernel.Print("?SYNTAX ERROR");
                    break;
            }
            if (!programmed) Kernel.Print("\rREADY.");
        }
        private static void Save(string line)
        {
            int numLen = 0;
            string numStr = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i].ToString(), out int _)) numStr += line[i];
                else break;
            }
            numLen = int.Parse(numStr);
            lines[numLen] = line.Remove(0, numStr.Length);
        }
    }
}