using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.gui.components.menu;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class ShopMenu
    {
        public static void Display(int cursorIndex = 0, string shopMessage = default)
        {
            // Get Player & Shop & Shop assortment
            Player player = Program.Player;
            VillageShop shop = player.VillagesManager.CurrentVillage.Shop;
            IList<IItem> shopItems = shop.GetAvailableItems();

            // Define menu items
            var menuItems = new Dictionary<string, Action<int>>();
            
            // Declare menu items
            foreach (IItem item in shopItems)
            {
                string label = $"\"{item.Name}\" for ${NumberConvertor.ShortenNumber(item.PriceForPlayer)} | { item.Amount } left";
                
                bool added = menuItems.TryAdd(label, (selectedIndex) =>
                {
                    // Check if player has enough money to afford this item
                    double moreMoney = player.MoneyManager.CanAfford(item.PriceForPlayer);
                    
                    // Break if moreMoney > 0, because it means that player has not enough money
                    if (moreMoney > 0)
                    {
                        Display(selectedIndex, $"You need { NumberConvertor.ShortenNumber(moreMoney) } more to buy \"{ item.Name }\"");
                    }
                    
                    // Remove item from the shop
                    IItem soldItem = shop.SellItem(item);
                    
                    // Add item to the player's inventory (take money from player)
                    player.BuyItem(soldItem);

                    // Refresh the menu by redrawing -> nonlinear recursion
                    ShopMenu.Display(selectedIndex);
                });

                if(!added) throw new ApplicationException("Item duplication >> VillageShop");
            }

            // Add go to menu menu itme
            menuItems.Add("> Go to menu <", (selectedIndex) => ActionGroupsMenu.Display());
            
            // Process shop message if valid
            if (shopMessage != default)
            {
                menuItems.Add(shopMessage, (selectedIndex) => Display());
            }

            // Display
            (new Menu(menuItems, "BUY IN THE SHOP:", cursorIndex)).Display();
        }
    }
}