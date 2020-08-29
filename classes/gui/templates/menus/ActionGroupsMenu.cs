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
            var items = new Dictionary<string, Action>()
            {
                {
                    "> Go to Shop <", () => ShopMenu.Display()
                },
                {
                    "> Sleep <", PlayerSleepProgress.Display
                },
                {
                    "> Fight Monsters <", () =>
                    {
                        // TODO: Monsters menu
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Open Inventory <", PlayerInventoryMenu.Display
                },
                {
                    "> Socialize <", () =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Move to Anoher Village <", TravelToVillageMenu.Display
                },
            };

            // Display
            (new Menu(items, "ACTIONS:")).Display();
        }
    }
}