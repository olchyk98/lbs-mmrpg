using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;

namespace lbs_rpg.classes.gui.templates
{
    public static class ActionGroupsMenu
    {
        public static void Display()
        {
            Dictionary<string, Action> items = new Dictionary<string, Action>()
            {
                {
                    "> Go to shop <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
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
                    "> Move to another village <", () =>
                    {
                        TravelToVillageMenu.Display();
                        return;
                    }
                },
            };

            (new Menu(items, "ACTIONS:")).Display();
        }
    }
}