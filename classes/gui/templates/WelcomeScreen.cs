using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lbs_mmrpg.classes.gui.components;
using lbs_mrpg.classes.gui.components;

namespace lbs_mmrpg.classes.gui.templates
{
    public static class WelcomeScreen
    {
        public static async Task Display()
        {
            // Clear the console
            FastGuiUtils.ClearConsole();
            
            // Create the breakline pipe
                // Initialize the string builder. We use it since we'll direct access to the char[]
            var breakLineBuilder = new StringBuilder(new string('═', Console.WindowWidth));

                // Change first and last char to the '*', for style purposes.
            breakLineBuilder[0] = '╠';
            breakLineBuilder[breakLineBuilder.Length - 1] = '╣';
            
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

            // Output the welcome text using FastGuiUtils tool.
            await FastGuiUtils.PrintCenteredTextWithAnimation(welcomeMessage, 10, breakLineIndexes);
        }
    }
}