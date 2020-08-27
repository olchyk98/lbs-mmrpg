using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.instances.player;

namespace lbs_rpg.classes.gui.templates
{
    public static class PlayerSleepProgress
    {
        public static void Display()
        {
            // Setting & Display the progress screen
            ConstantProgress.Start("SLEEPING", 250,  () =>
            {
                // Update player's stats
                Program.Player.GainSleep(1);
            });
            
            // Move to the main menu when the process were interrupted 
            ActionGroupsMenu.Display();
        }
    }
}