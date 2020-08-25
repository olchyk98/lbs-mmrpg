using System;
using lbs_rpg.classes.gui.components.colorize;
using lbs_rpg.classes.instances.player;

namespace lbs_rpg.classes.gui.components
{
    public static class StatsBar
    {
        /// <summary>
        /// Renders a stats bar on the last and pre-last y positions
        /// </summary>
        public static void Display(Player player)
        {
            // Health Bar
            DisplayHealthBar();
            
            // Stats
            DisplayStats();
        }

        // Sequential Method
        private static void DisplayHealthBar()
        {
            // HealthBar Y position
            int posY = ResolutionHandler.GetResolution(1) - 1;
            
            // Line width
            int width = ResolutionHandler.GetResolution(0);
            
            // Player current health in %
            string health = Program.Player.GetHealthProcentString();
            
            // Health text position X
            int healthX = width / 2 - health.Length / 2;
            
            // Health bar (rectangle and the text)
            string barText = "";
            
            // Content the barText (rectangles and text)
            for (var ma = 0; ma < width; ma++)
            {
                if (ma >= healthX && ma < healthX + health.Length) barText += health[ma - healthX];
                else barText += ' ';
            }

            // Place cursor at the position
            Console.SetCursorPosition(0, posY);
            
            // Display the health bar rectangle
            Console.WriteLine(barText.Colorize("bgred"));
        }

        private static void DisplayStats()
        {
            /*
             * Money, Protection, Social Level
             * [] 100       [] 200       100 []
             */
            
            // Stats Y position
            int posY = ResolutionHandler.GetResolution(1) - 4;
            
            // Space gap between elements
            string elementsGap = new string(' ', 4);
            
            // Output content
            string elements =  String.Join("", $"💵 4.6k", elementsGap, $"🛡️ 4.1%", elementsGap, $"🧑 35/1500");
            
            // Calculate position (center)
            int posX = ResolutionHandler.GetResolution(0) / 2 - elements.Length / 2;
            
            // Set cursor position
            Console.SetCursorPosition(posX, posY);
            
            // Output
            Console.WriteLine(elements);
        }
    }
}