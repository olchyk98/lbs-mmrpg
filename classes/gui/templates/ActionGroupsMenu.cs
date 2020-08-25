using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;

namespace lbs_rpg.classes.gui.templates
{
    public static class ActionGroupsMenu
    {
        // Inefficient.
        // In theory, the Program class can contain a static list/dictionary of IMenu [REFACTOR]
        public static void Display(bool clearConsole = true)
        {
            Dictionary<string, Action> items = new Dictionary<string, Action>()
            {
                {
                    "> Shop <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Sleep <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Go to the dungeonc <", () =>
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
            };

            (new Menu(items, "ACTIONS:")).Display(clearConsole);
        }
    }
}