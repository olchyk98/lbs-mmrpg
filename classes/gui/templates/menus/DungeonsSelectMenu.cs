using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.gui.templates.custom;
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
            Type monsterInterfaceType = typeof(IMonster);
            List<Type> monsterTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ma => ma.GetTypes())
                .Where(ma => monsterInterfaceType.IsAssignableFrom(ma) && !ma.IsInterface).ToList();
            
            // Fill the options list
            foreach (Type monsterType in monsterTypes)
            {
                // Get name property from the type
                PropertyInfo monsterInfo = monsterType.GetProperty("Name", (BindingFlags.Public | BindingFlags.Static));
                
                // Extract monster name
                string monsterName = monsterInfo?.GetValue(null, null)?.ToString();

                // Skip if there's a problem with the project structure
                if (monsterName == null)
                {
                    // TODO: Report with Debugger
                    continue;
                }

                // Add monster to the menu
                string itemLabel = $"Dungeon \"{monsterName}\"";
                
                menuItems.Add(itemLabel, (selectedIndex) =>
                {
                    PlayerDungeonFight.Display(monsterType);
                });
            }

            // throw new Exception();
            
            // Display menu
            (new Menu(menuItems, "GO TO DUNGEON:")).Display();
        }
    }
}