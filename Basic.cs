using System.Diagnostics;

namespace Emil
{
    public class Basic
    {
        static Dictionary<int, string> lines = new Dictionary<int, string>();
        public static void Do(string line)
        {
            if (int.TryParse(line.Split(' ')[0], out int lineNr)) Store(line.Remove(0, line.Split(' ')[0].Length + 1), lineNr);
            else
            {
                Execute(line);
                Vars.cursorPos += 40;
                Print("READY.");
                Vars.cursorPos += 40;
            }
        }
        private static void Store(string line, int lineNr)
        {
            Debug.WriteLine(lineNr + ": " + line);
            lines.Add(lineNr, line);
        }
        private static void Execute(string line)
        {
            Debug.WriteLine(line);
            string[] parts = line.Split(' ');
            switch (parts[0])
            {
                case "PRINT":
                    if (line.Split('"').Length == 3)
                    {
                        Print(line.Split('"')[1]);
                        Vars.cursorPos += 40;
                    }
                    else
                    {
                        Print("?SYNTAX ERROR");
                        Vars.cursorPos += 40;
                    }
                    break;
                default:
                    Print("?SYNTAX ERROR");
                    Vars.cursorPos += 40;
                    break;
            }
        }
        private static void Print(string print)
        {
            char[] chars = print.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] + Vars.charSet);
            }
            print = new String(chars);
            Vars.screenRam = arrayInsert(Vars.screenRam, print, Vars.cursorPos);
        }
        private static char[] arrayInsert(char[] array, string toInsert, int pos)
        {
            for (int i = 0; i < toInsert.Length; i++)
            {
                array[pos + i] = toInsert[i];
            }
            return array;
        }
    }
}