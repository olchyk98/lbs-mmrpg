using System;
using System.Collections.Generic;
using lbs_rpg.classes.gui.components;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.gui.templates.menus
{
    public static class ShopMenu
    {
        public static void Display()
        {
            // Get Player & Shop & Shop assortment
            Player player = Program.Player;
            VillageShop shop = player.VillagesManager.CurrentVillage.Shop;
            IList<IItem> shopItems = shop.GetAvailableItems();

            // Define menu items
            var menuItems = new Dictionary<string, Action>();
            
            // Declare menu items
            foreach (IItem item in shopItems)
            {
                string label = $"\"{item.Name}\" for ${NumberConvertor.ShortenNumber( (int) item.Price )} | { item.Amount } left";
                
                bool added = menuItems.TryAdd(label, () =>
                {
                    IItem soldItem = shop.SellItem(item);
                    player.BuyItem(soldItem);
                });

                if(!added) throw new ApplicationException("Item duplication >> VillageShop");
            }
            
            // Add go to menu menu itme
            menuItems.Add("> Go to menu <", ActionGroupsMenu.Display);

            // Display
            (new Menu(menuItems, "BUY IN THE SHOP:")).Display();
        }
    }
}