using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.gui.templates.progress
{
    public static class PlayerTravelProgress
    {
        public static void Display(Village targetVillage)
        {
            // Get player
            Player player = Program.Player;

            // Get number of ticks that are needed to move to the village
            // .GetDistanceToInTicks(targetVillage, player)
            int numberOfTicks = player.VillagesManager.GetDistanceToInTicks(targetVillage);

            // Get distance to the target village in km
            double distanceToVillage = player.VillagesManager.CurrentVillage.GetDistanceTo(targetVillage);

            // Define menu title
            string menuTitle = $"Traveling to \"{targetVillage.Name}\"";
            
            // Calculate health reduction per tick
            float damagePerTick = player.VillagesManager.GetTripHealthRequirement(targetVillage) / numberOfTicks;

            // Display progress
            ConstantProgress.Start(menuTitle, 200, numberOfTicks, (isDone) =>
            {
                // Apply trip damage to the player
                player.ApplyDamage(damagePerTick);

                if (isDone)
                {
                    // Move player to the target village
                    player.VillagesManager.Migrate(targetVillage);
                }
            }, (doneProcent) =>
            {
                return $"{(distanceToVillage - distanceToVillage * doneProcent) :0}km left";
            });
        }
    }
}