using System;

namespace Akka.Common
{
    public static class ColorConsole
    {
        public static void WriteRedLine(string message)
        {
            WriteLineColored(message, () => ConsoleColor.Red);
        }

        public static void WriteCyanLine(string message)
        {
            WriteLineColored(message, () => ConsoleColor.Cyan);
        }

        public static void WriteGreenLine(string message)
        {
            WriteLineColored(message, () => ConsoleColor.Green);
        }

        public static void WriteLineYellow(string message)
        {
            WriteLineColored(message, () => ConsoleColor.Yellow);

        }

        private static void WriteLineColored(string message, Func<ConsoleColor> getColor)
        {
            var colorBefore = Console.ForegroundColor;

            Console.ForegroundColor = getColor();

            Console.WriteLine(message);

            Console.ForegroundColor = colorBefore;
        }
    }
}
