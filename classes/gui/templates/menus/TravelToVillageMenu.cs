using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.gui.templates.progress;
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
            foreach (Village village in player.VillagesManager.GetNearestVillages(5))
            {
                // Extract some values to construct the option label
                double distance = village.GetDistanceTo(currentVillage);
                float healthRequirement = player.VillagesManager.GetTripHealthRequirement(village);
                string reputationLabel = player.VillagesManager.GetVillageReputationAsString(village);

                // Generate option label
                string optionTitle =
                    $"\"{village.Name}\" ({distance:0.0}km) ({reputationLabel} Reputation) | -{healthRequirement:0.0}hp";

                // Add to the menu
                menuItems.Add(optionTitle, (selectedIndex) =>
                {
                    // Start traveling
                    PlayerTravelProgress.Display(village);
                });
            }

            // Add "back to menu" button
            menuItems.Add("> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display());

            // Display
            (new Menu(menuItems, "TRAVEL TO:")).Display();
        }
    }
}