// TODO: Refactor -> IDE Style
// TODO: Refactor -> Documentation [///]
// TODO: Refactor -> Class Regions
// TODO [low priority]: Refactor code using LBS Style Guide ("my...", "a...")

using System;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.templates;
using lbs_rpg.classes.instances.player;

namespace lbs_rpg
{
    public static class Program
    {
        public static Player Player = default;
        
        public static void Main(string[] args)
        {
            // Check if terminal container is not too small
            if (!ResolutionHandler.IsSupportedResolution())
            {
                throw new ApplicationException(
                    "The terminal window is too small. Please change window to the fullscreen mode.");
            }
            
            // Initialize player instance
            Player = new Player();
            
            // Display the welcome message (game introduction)
            WelcomeScreen.Display();
            
            // Clear Console
            FastGuiUtils.ClearConsole();
            
            ConstantProgress.Start(250, () =>
            {
                // Redraw Player Stats
                FastGuiUtils.ClearConsole();
                StatsBar.Display(Program.Player);
            });
            return;
            
            // Display the stats (before the menu, since the menu method will block the thread)
            // TODO: Stats should also display the enemy damage (not in the Program.Main)
            // TODO: Stats should accept an argument [isAttackReloadShown] and replace the money field with the attack reload
                // * It would be super cool during the fights
            StatsBar.Display(Player);
            
            // Display the main menu
            ActionGroupsMenu.Display(false);
        }
    }
}
