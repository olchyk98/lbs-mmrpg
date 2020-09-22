// TODO [LP]: Debug to file -> Add during refactoring
    // IF ACCEPTED: Name class "Debugger" -> *Static

// TODO [LP]: Refactor -> Split DungeonEngine (partials) into classes ;; Create subclasses for the dungeon engine
//     (renderer, updater, collector, keyboard controller) -> With ref to the parent instance

// TODO: Fill welcome message
// TODO: Add items
// TODO: Check language

/*
 * Player should get good reputation in a village to win.
 * He can gain reputation by buying stuff from the shop, fighting monsters to protect the village or
 * socializing.
 *
 * The better reputation the player has in a village the lower prices are in [a] village shop.
 *
 * Player can buy:
 *     Books (to be able to gain more social points during the socializing process)
 *     Armor (to get more defense points, defense procent)
 *     Attack range (increases the attack wave range)
 *     Level upgrade (the health and walkingstamina increases)
 * Player can get money:
 *     By fighting monsters and bringing their heads to the shop.
 *     For every head the player gets [] points of reputation.
 *     Every head (every monster) has it's price. The harder monster to kill the bigger price is.
 *
 * Health represents both the energy and the health itself.
 * So when player's traveling the health decreases.
 * TODO: Declare Player.WalkingStamina, the bigger the value is the less health it takes per 1km
 *
 * Player can play fast, choose a village and just help them with everything,
 * or he can go from village to village and find new artifacts, stuff and monsters.
 * 
 */

using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.templates.menus;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.contracts.gui;

namespace lbs_rpg
{
    public static class Program
    {
        // Declare few game important instances. They can be accessed from any class in the program.
        public static Player Player = default;

        // Declare fast villages instance reference
        public static GameVillages Villages = default;

        // Declare RenderPipeline that includes renderable items (such as player stats) that will
        // be redrawn after each console.clear invocation.
        public static readonly IList<IRenderable> RenderPipeline = new List<IRenderable>();

        public static void Main(string[] args)
        {
            // Check if terminal container is not too small
            if (!ResolutionHandler.IsSupportedResolution())
            {
                throw new ApplicationException(
                    "The terminal window is too small. Please change window to the fullscreen mode.");
            }

            // Initialize player/villages instances
            Villages = new GameVillages();
            Player = new Player(Villages.GetRandomVillage());

            // Initialize render pipeline
            RenderPipeline.Add(new PlayerStatsBar());

            // Display the welcome message (game instructions)
            // WelcomeScreen.Display();

            // Display the main menu
            while (true)
            {
                /*
                // Constantly invoke main menu printing when thread is not busy
                // That way every task (other menu) can avoid memory recursion and
                // just skip the task, knowing that program will navigate user
                // to the main menu anyway.
                */
                ActionGroupsMenu.Display();
            }
        }
    }
}