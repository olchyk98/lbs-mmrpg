using System;
using System.Collections.Generic;
using System.Linq;
using lbs_rpg.classes.gui.components.dungeonengine;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class DungeonsSelectMenu
    {
        public static void Display()
        {
            // Define menu items dictionary
            var menuItems = new Dictionary<string, Action<int>>();
            
            // Get all monsters in the assembly to access names
            Type monsterInterfaceType = typeof(AMonster);
            List<Type> monsterTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ma => ma.GetTypes())
                .Where(ma => monsterInterfaceType.IsAssignableFrom(ma) && !ma.IsInterface).ToList();
            
            // Fill the options list
            foreach (Type monsterType in monsterTypes)
            {
                // Get name property from the type
                var monsterName = (string) monsterType.GetProperty("Name")?.GetValue(null);
                
                // Get monster strength level
                var monsterLevel = (string) monsterType.GetField("FightDifficulty")?.GetValue(null);
                
                // Skip if there's a problem with the project structure
                if (monsterName == null)
                {
                    continue;
                }

                // Add monster to the menu
                string itemLabel = $"Dungeon \"{monsterName}\" (Difficulty: {monsterLevel})";
                
                menuItems.Add(itemLabel, (selectedIndex) =>
                {
                    (new DungeonEngine(monsterType)).ProcessTicks();
                });
            }
            
            // Add go to menu menu itme
            menuItems.Add("> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display());

            // Display menu
            (new Menu(menuItems, "GO TO DUNGEON:")).Display();
        }
    }
}