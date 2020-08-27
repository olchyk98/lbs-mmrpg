using System;
using System.Collections.Generic;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts;

namespace lbs_rpg.classes.instances.villages
{
    public class VillageShop
    {
        #region Fields
        // Shop stock
        /// <summary>
        /// Use VillageShop.GenerateStock to generate value for this variable
        /// </summary>
        private IList<IItem> _items;
        
        // Based on this value, the shop will display different items
        private DateTime _lastUpdateTime = DateTime.Now;
        
        // Difference in time between stock updates
        private const int STOCK_REFRESH_TIME = 80 * 1000; // 80 seconds
        
        // Cache Random
        private static readonly Random Random = new Random();
        #endregion

        #region Constructor
        public VillageShop(IList<IItem> stock)
        {
            _items = stock;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Regenerates stock state and randomizes some values,
        /// such as amount of different items, new items in the stock.
        /// </summary>
        public void RegenerateStock()
        {
            // Create a fast variable, in case if I will change the target.
            IList<IItem> stock = _items;
            
            // Randomize item positions (just for a visible effect)
            stock.Shuffle();
            
            // Randomize items amount
            for (int ma = 0; ma < stock.Count; ++ma)
            {
                int randomAmount = Random.Next(0, 20);

                // Remove if amount is 0
                if (randomAmount == 0)
                {
                    stock.RemoveAt(ma);
                    continue;
                }
                
                // Update item amount
                stock[ma].SetAmount(randomAmount);
            }
            
            // Add new items
            int newItemsCount = (Random.Next(0, 1) == 0) ? 0 : Random.Next(1, 4);

            for (var ma = 0; ma < newItemsCount; ++ma)
            {
                stock.Add(GenerateItem());
            }
        }

        /// <summary>
        /// Check if the stock should be refreshed and refreshes it if yes.
        /// </summary>
        /// <returns>
        /// Current items in the stock as List
        /// </returns>
        public IList<IItem> GetAvailableItems()
        {
            // Check if stock should be refreshed, by checking time.
            if (DateTime.Now > _lastUpdateTime.AddMilliseconds(STOCK_REFRESH_TIME))
            {
                _lastUpdateTime = DateTime.Now;
                RegenerateStock();
            }
            
            // Return stock state
            return _items;
        }
        
        /// <summary>
        /// Generates (Randomizes) single item
        /// </summary>
        /// <returns>
        /// Generated item.
        /// </returns>
        private static IItem GenerateItem()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Generates init state for a shop.
        /// </summary>
        /// <returns>
        /// List of generated items
        /// </returns>
        public static IList<IItem> GenerateStock()
        {
            // Define items holder
            IList<IItem> stock = new List<IItem>();
            
            // Randomize array length
            int itemsCount = Random.Next(2, 8);

            for (int ma = 0; ma < itemsCount; ++ma)
            {
                stock.Add(GenerateItem());
            }
            
            // Return generted items
            return stock;
        }
        #endregion
    }
}