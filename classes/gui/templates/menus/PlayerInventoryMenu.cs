using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class PlayerInventoryMenu
    {
        public static void Display()
        {
            // Access player & playerInventory
            Player player = Program.Player;
            PlayerInventory playerInventory = player.Inventory;
            Village playerVillage = player.VillagesManager.CurrentVillage;
            
            // Define menu items dictionary
            var menuItems = new Dictionary<string, Action>();
            
            // Add items
            foreach (IItem item in playerInventory.Items)
            {
                // Add equip option
                if (item is IEquipable equipableItem)
                {
                    // Check if item is already equiped. Prepare for unequip action if yes
                    bool doEquip = !equipableItem.IsEquipedOn(playerInventory);
                    
                    // Action label
                    string equipText = (doEquip) ? "Equip" : "Unequip";
                
                    // Add option to the menu
                    menuItems.Add($"{ equipText } \"{ item.Name }\"", () =>
                    {
                        // Equip/Unequip
                        if (doEquip) equipableItem.EquipOn(playerInventory);
                        else equipableItem.UnequipOn(playerInventory);
                        
                        // Refresh menu
                        Display();
                    });
                }
                
                // Add sell option
                menuItems.Add($"Sell \"{ item.Name }\" for ${ NumberConvertor.ShortenNumber(item.SellPriceForPlayer) } | You have { item.Amount }", () =>
                {
                    player.SellItem(item);
                    
                    // Refresh menu
                    Display();
                });
            }
            
            // Add "back to menu" button
            menuItems.Add("> Go to menu <", ActionGroupsMenu.Display);

            // Display
            (new Menu(menuItems, "INVENTORY ACTIONS:")).Display();
        }
    }
}