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
                "Welcome to some bulshit game.",
                "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.",
                "The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here',",
                "making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text,",
                "and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years,",
                "sometimes by accident, sometimes on purpose (injected humour and the like).",
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