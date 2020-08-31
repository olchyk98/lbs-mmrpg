using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.gui.templates.progress;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class VillageReputationTasksMenu
    {
        public static void Display(int cursorPosition = 0)
        {
            // Ref player & village
            Player player = Program.Player;
            PlayerVillage villageManager = player.VillagesManager;

            // Define menu options dictionary
            var menuItems = new Dictionary<string, Action<int>>();
            
            // Get available tasks
            IList<VillageTask> availableTasks = villageManager.CurrentVillage.Tasks.GetAvailableTasks();

            // Get all village's tasks
            foreach (VillageTask task in availableTasks)
            {
                // Skip if player cannot do this task
                if (!villageManager.CanDoTask(task)) continue;

                // Define menu option title
                string optionLabel =
                    $"{task.Description} | +{task.ReputationBonus} Reputation | -{villageManager.GetTaskHealthRequirement(task) :0.0}hp";

                // Add option to the menu
                menuItems.Add(optionLabel, (selectedIndex) =>
                {
                    // Start doing task
                    PlayerDoTaskProgress.Display(task);
                });
            }
            
            // Add no tasks message
            if (availableTasks.Count == 0)
            {
                menuItems.Add("No tasks available right now. Maybe sleeping will help", Display);
            }

            // Add "back to menu" button
            menuItems.Add("> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display());

            // Display
            (new Menu(menuItems, "SOCIALIZE:", cursorPosition)).Display();
        }
    }
}