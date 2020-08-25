using System;
using lbs_rpg.classes.gui.components.colorize;

namespace lbs_rpg.classes.gui.components
{
    public static partial class FastGuiUtils
    {
        /// <summary>
        /// Determines how big container does array need,
        /// and centers (prints) all the array items.
        /// </summary>
        /// <param name="text">
        /// May be colorized using ANSI characters.
        ///
        /// NOTICE: This project has its own built-in class called Colorize. Use it to change the string style.
        /// </param>
        /// <param name="overrideScreen">
        /// Override screen content instead of erasing it.
        /// </param>
        public static void PrintCenteredText(string[] text)
        {
            int textLines = text.Length;
            int[] windowDimensions = {Console.WindowWidth, Console.WindowHeight};

            // Iterate through all characters in the array.
            for (var ma = 0; ma < text.Length; ++ma)
            {
                // TODO [completed]: All ANSI characters should be removed from the XString, since they are not visible.
                string line = text[ma];
                // Calculate X and Y axis for the character
                int x = (windowDimensions[0] / 2 - line.Decolorize().Length / 2);
                int y = (windowDimensions[1] / 2 - textLines / 2 + ma);
                //
                Console.SetCursorPosition(x, y);
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Erases the console window.
        /// This is an external extension method, since I'm planing to make a static render pipeline.
        /// </summary>
        public static void ClearConsole()
        {
            // Erase current output
            Console.Clear();
        }
    }
}