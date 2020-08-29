using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.components.menu;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class TravelToVillageMenu
    {
        public static void Display()
        {
            // Show only nearest villages to the player
            Dictionary<string, Action<int>> items = new Dictionary<string, Action<int>>()
            {
                {
                    "> Rovino (41.6km) <", (selectedIndex) =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Setup (21.4km) <", (selectedIndex) =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display()
                }
            };

            for (var ma = 0; ma < 100; ++ma)
            {
                items.Add($"> DO HELLO: {ma}", (selectedIndex) => { });
            }
            
            // Display
            (new Menu(items, "TRAVEL TO:")).Display();
        }
    }
}