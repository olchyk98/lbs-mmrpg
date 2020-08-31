using lbs_rpg.classes.gui.components;

namespace lbs_rpg.classes.gui.templates.progress
{
    public static class PlayerSleepProgress
    {
        public static void Display()
        {
            // Setting & Display the progress screen
            ConstantProgress.Start("Sleeping", 250,  () =>
            {
                // Update player's stats
                Program.Player.GainSleep(1);
            });
            
            // Move to the main menu when the process were interrupted 
            return;
        }
    }
}