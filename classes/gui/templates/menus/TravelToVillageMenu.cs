using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class TravelToVillageMenu
    {
        public static void Display()
        {
            // Ref player & currentVillage
            Player player = Program.Player;
            Village currentVillage = player.VillagesManager.CurrentVillage;
            
            // Show only nearest villages to the player
            var menuItems = new Dictionary<string, Action<int>>();

            // Add nearest villages to the menu
            foreach (Village village in player.GetNearestVillages(5))
            {
                menuItems.Add($"\"{ village.Name }\" ({ village.GetDistanceToAsKm(currentVillage) :0.0}km)", (selectedIndex) =>
                {
                    Console.WriteLine("a");
                });
            }
            
            // Add "back to menu" button
            menuItems.Add("> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display());
            
            // Display
            (new Menu(menuItems, "TRAVEL TO:")).Display();
        }
    }
}