using System;
using System.Text;
using lbs_rpg.classes.gui.components.colorize;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.gui.components
{
    public class PlayerStatsBar : IRenderable
    {
        #region Fields
        private Player myTargetPlayer = default;
        #endregion

        #region Constructor
        public PlayerStatsBar(Player player)
        {
            myTargetPlayer = player;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders a stats bar on the last and pre-last y positions
        /// </summary>
        public void Display()
        {
            // Health Bar
            DisplayHealthBar();
            
            // Current location (village)
            DisplayLocation();

            // Stats
            DisplayStats();
        }

        // Sequential Methods
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

                // Place text char when in the text container area
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

        private void DisplayLocation()
        {
            // HealthBar Y position
            int posY = ResolutionHandler.GetResolution(1) - 6;

            // Line width
            int width = ResolutionHandler.GetResolution(0);
            
            // Declare output content
            string content = $"Village \"{myTargetPlayer.VillagesManager.CurrentVillage.Name}\"".Colorize("white");
            
            // Set print cursor
            Console.SetCursorPosition(width / 2 - content.Decolorize().Length / 2, posY);
            
            // Output
            Console.WriteLine(content);
        }
        
        private void DisplayStats()
        {
            /*
             * [] 100       [] 200       [] 100
             */

            // Access player instance
            Player player = Program.Player;
            
            // Access player's village manager. Cache it since we will use it twice
            PlayerVillage villageManager = player.VillagesManager;

            // Stats Y position
            int posY = ResolutionHandler.GetResolution(1) - 4;

            // Space gap between elements
            string elementsGap = new string(' ', 4);

            // Declare hud elements
            string[] elements =
            {
                $"Money: {NumberConvertor.ShortenNumber(player.MoneyManager.Money)}",
                $"Protection:  {player.Stats.DefenseProcent}%",
                $"Speed: {player.Stats.MovementSpeed}",
                $"Reputation: {villageManager.GetReputation()}/{villageManager.CurrentVillage.MaxReputation}"
            };

            // string elements =  string.Join(string.Empty, $"", elementsGap, $"🛡️ 4.1%", elementsGap, $"🧑 35/1500");
            // Convert hud elements array to string
            string elementsString = string.Join(new string(' ', 8), elements);

            // Calculate position (center)
            int posX = ResolutionHandler.GetResolution(0) / 2 - elementsString.Length / 2;

            // Set cursor position
            Console.SetCursorPosition(posX, posY);

            // Output
            Console.WriteLine(elementsString);
        }
        #endregion
    }
}