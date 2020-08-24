using System;
using System.Collections.Generic;
using lbs_mrpg.classes.gui.components;

namespace lbs_mmrpg.classes.gui.templates
{
    public class GameMenu : Menu
    {
        public GameMenu() : base(ConfigureMenu())
        { }

        private static Dictionary<string, Action> ConfigureMenu()
        {
            return new Dictionary<string, Action>()
            {
                {
                    "hello", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "h3ello", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "h4ello", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "h5ello", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
            };
        }
    }
}