using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.gui.templates.progress;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class TravelToVillageMenu
    {
        public static void Display(int cursorPosition = 0)
        {
            // Ref player & currentVillage
            Player player = Program.Player;
            Village currentVillage = player.VillagesManager.CurrentVillage;

            // Show only nearest villages to the player
            var menuItems = new Dictionary<string, Action<int>>();

            // Get nearest villages
            IList<Village> nearestVillages = player.VillagesManager.GetNearestVillages(5);

            // Add nearest villages to the menu
            foreach (Village village in nearestVillages)
            {
                // Extract some values to construct the option label
                double distance = village.GetDistanceTo(currentVillage);
                float healthRequirement = player.VillagesManager.GetTripHealthRequirement(village);
                string reputationLabel = player.VillagesManager.GetReputationAsString(village);

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
            
            // Not enough health to travel message
            if (nearestVillages.Count == 0)
            {
                menuItems.Add("You cannot travel anywhere, because you are low", Display);
            }

            // Add "back to menu" button
            menuItems.Add("> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display());

            // Display
            (new Menu(menuItems, "TRAVEL TO:", cursorPosition)).Display();
        }
    }
}