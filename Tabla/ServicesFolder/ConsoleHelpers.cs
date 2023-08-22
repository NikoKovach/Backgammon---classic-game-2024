namespace Tabla.ServicesFolder
{
    using System;

    public static class ConsoleHelpers
    {
        public static void CenterCursorOnConsole(int lenghtOfText)
        {
            int centerRow = Console.WindowHeight / 2;
            int centerCol = Console.WindowWidth / 2 - lenghtOfText/2;
            Console.SetCursorPosition(centerCol, centerRow);
        }
    }
}
