using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.templates.progress;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class ActionGroupsMenu
    {
        public static void Display()
        {
            // Declare menu items
            Dictionary<string, Action> items = new Dictionary<string, Action>()
            {
                {
                    "> Go to shop <", ShopMenu.Display
                },
                {
                    "> Sleep <", PlayerSleepProgress.Display
                },
                {
                    "> Go to the dungeon <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Socialize <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Move to another village <", TravelToVillageMenu.Display
                },
            };

            // Display
            (new Menu(items, "ACTIONS:")).Display();
        }
    }
}