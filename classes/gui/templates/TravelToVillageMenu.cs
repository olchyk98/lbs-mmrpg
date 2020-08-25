using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;

namespace lbs_rpg.classes.gui.templates
{
    public class TravelToVillageMenu
    {
        public static void Display(bool clearConsole = true)
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
            };

            // Create a renderpipeline (better), or put RenderStats in the menu's display method (bad)
            (new Menu(items, "ACTIONS:")).Display();
        }
    }
}