using System;
using System.Text;
using lbs_rpg.classes.gui.components.colorize;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.gui.components
{
    public class PlayerStatsBar : IRenderable
    {
        private Player _targetPlayer = default;

        public PlayerStatsBar(Player player)
        {
            _targetPlayer = player;
        }
        
        /// <summary>
        /// Renders a stats bar on the last and pre-last y positions
        /// </summary>
        public void Display()
        {
            // Health Bar
            DisplayHealthBar();
            
            // Stats
            DisplayStats();
        }

        // Sequential Method
        private void DisplayHealthBar()
        {
            // HealthBar Y position
            int posY = ResolutionHandler.GetResolution(1) - 1;
            
            // Line width
            int width = ResolutionHandler.GetResolution(0);
            
            // Get player health in %, outputable form
            int playerHealthProcent = Program.Player.GetHealthProcent();
            string health = Program.Player.HealthToString();
            
            // Health text position X
            int healthX = width / 2 - health.Length / 2;
            
            // Health bar (rectangle and the text)
            StringBuilder barTextBuilder = new StringBuilder();
            
            // Content the barText (rectangles and text)
            for (var ma = 0; ma < width; ma++)
            {
                // Chat that will be added during this iteration
                string current = " ";

                // Place text char when at the right position
                if (ma >= healthX && ma < healthX + health.Length)
                {
                    current = health[ma - healthX].ToString();
                }

                // Checks if this position in the healthbar should be filled (health)
                if (ma < Math.Floor(playerHealthProcent / 100f * width))
                {
                    current = current.Colorize("bgred");
                }

                // Append char to the output
                barTextBuilder.Append(current);
            }

            // Place cursor at the position
            Console.SetCursorPosition(0, posY);
            
            // Display the health bar rectangle
            string barText = barTextBuilder.ToString();
            Console.WriteLine(barText);
        }

        private void DisplayStats()
        {
            /*
             * [] 100       [] 200       [] 100
             */
            
            // Stats Y position
            int posY = ResolutionHandler.GetResolution(1) - 4;
            
            // Space gap between elements
            string elementsGap = new string(' ', 4);
            
            // Output content
            string elements =  String.Join(string.Empty, $"💵 4.6k", elementsGap, $"🛡️ 4.1%", elementsGap, $"🧑 35/1500");
            
            // Calculate position (center)
            int posX = ResolutionHandler.GetResolution(0) / 2 - elements.Length / 2;
            
            // Set cursor position
            Console.SetCursorPosition(posX, posY);
            
            // Output
            Console.WriteLine(elements);
        }
    }
}