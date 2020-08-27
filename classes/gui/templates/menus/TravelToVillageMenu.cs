using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class TravelToVillageMenu
    {
        public static void Display()
        {
            // Show only nearest villages to the player
            Dictionary<string, Action> items = new Dictionary<string, Action>()
            {
                {
                    "> Rovino (41.6km) <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Setup (21.4km) <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Go to menu <", ActionGroupsMenu.Display
                }
            };
            
            // Display
            (new Menu(items, "TRAVEL TO:")).Display();
        }
    }
}