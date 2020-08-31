using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.gui.templates.progress
{
    public static class PlayerDoTaskProgress
    {
        public static void Display(VillageTask task)
        {
            // Access player
            Player player = Program.Player;

            // Define menu title
            string menuTitle = $"Doing task: \"{task.Description}\"";

            // Calculate health reduction per tick
            float tickDamage = player.VillagesManager.GetTaskHealthRequirement(task) / task.DurationTicks;
            
            // Display progress
            ConstantProgress.Start(menuTitle, 200, task.DurationTicks, (isDone) =>
            {
                // Update player's health
                player.ApplyDamage(tickDamage);
                
                // Process last frame
                if (isDone)
                {
                    // Update player's reputation
                    player.VillagesManager.AddReputation(task.ReputationBonus);

                    // Remove task from the village
                    task.Village.Tasks.RemoveTask(task);
                }
            });
        }
    }
}