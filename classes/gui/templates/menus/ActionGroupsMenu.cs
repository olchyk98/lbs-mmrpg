using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.gui.templates.progress;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class ActionGroupsMenu
    {
        public static void Display()
        {
            // Declare menu items
            var items = new Dictionary<string, Action<int>>()
            {
                {
                    "> Go to Shop <", (selectedIndex) => ShopMenu.Display()
                },
                {
                    "> Sleep <", (selectedIndex) => PlayerSleepProgress.Display()
                },
                {
                    "> Fight Monsters <", (selectedIndex) =>
                    {
                        // TODO: Monsters menu
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Open Inventory <", (selectedIndex) => PlayerInventoryMenu.Display()
                },
                {
                    "> Socialize <", (selectedIndex) =>
                    {
                        Console.WriteLine("a");
                        return;
                    }
                },
                {
                    "> Move to Anoher Village <", (selectedIndex) => TravelToVillageMenu.Display()
                },
            };

            // Display
            (new Menu(items, "ACTIONS:")).Display();
        }
    }
}