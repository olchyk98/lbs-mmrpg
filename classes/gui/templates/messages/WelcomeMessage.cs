using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using lbs_rpg.classes.gui.components;

namespace lbs_rpg.classes.gui.templates.messages
{
    /// <summary>
    /// The method displays the welcome message,
    /// and waits for an input to continue.
    /// </summary>
    public static class WelcomeScreen
    {
        public static void Display()
        {
            // Clear the console
            FastGuiUtils.ClearConsole(true);

            // Create the breakline pipe
            // Initialize the string builder. We need a direct access to the char[]
            var breakLineBuilder = new StringBuilder(new string('═', ResolutionHandler.GetResolution(0)));

            // Change first and last char to the '*', for style purposes.
            breakLineBuilder[0] = '╠';
            breakLineBuilder[^1] = '╣';

            // Cache the stringBuilder output, since we will use this string more than once.
            string breakLine = breakLineBuilder.ToString();

            // Initialize array with the message
            string[] welcomeMessage =
            {
                breakLine,
                "Welcome, Warrior!",
                "Dark times have come.",
                "After this attack by strange monsters, people started to think that you came with them.",
                "Your reputation has fallen, no one trusts you know. Therefore it's your opportunity to make up for everyone.",
                "Travel between villages, fight monsters, and make people like you. The higher reputation you have the more things you can do.",
                "Your goal is to gain a maximal reputation in at least one village. Help people survive and help them.",
                "Good luck, Warrior! You'll need it.",
                breakLine,
                "Press X to start"
            };

            //
            var breakLineIndexes = new SortedSet<int>
            {
                0,
                welcomeMessage.Length - 2
            };

            // Declare a cancellationSource
            CancellationTokenSource cancellationSource = new CancellationTokenSource();

            // Output the welcome text using FastGuiUtils tool.
            // Put in a separate thread to be able to listen to the keyboard input in the current thread
            Task.Run(() =>
            {
                FastGuiUtils.PrintCenteredTextWithAnimation(cancellationSource, welcomeMessage, 10,
                    breakLineIndexes);
            });

            // boolean, that represents if the X (continue) button was pressed
            bool skipped = false;

            // Listen for input
            while (!skipped)
            {
                // Listen & Get the pressed key
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                // Check if the key is X
                if (keyPressed.Key == ConsoleKey.X) skipped = true;
            }

            // Cancel the drawing task after the 
            cancellationSource.Cancel();
        }
    }
}