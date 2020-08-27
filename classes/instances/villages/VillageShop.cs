using System;
using System.Collections.Generic;
using System.Linq;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.villages
{
    public class VillageShop
    {
        #region Fields

        private static IList<IItem> _stock = new List<IItem>();

        // Based on this value, the shop will display different items
        private DateTime _lastStockUpdateTime = DateTime.Now;

        // Value that represents how popular the store is at the moment (changes with time) (cannot be affected by player).
        // This value is used in the items price calculation.
        private float _currentPopularity;

        // Last time when the popularity value was updated.
        private DateTime _popularityUpdatedTime = DateTime.Now;

        // []: Described in the .CalculateShopPrice documentation
        private const float PRICE_MULTIPLIER = 1.2f;

        // Time difference between popularity updates
        private const int POPULARITY_REFRESH_TIME = 60 * 1000; // 60 seconds

        // Time difference between stock updates
        private const int STOCK_REFRESH_TIME = 80 * 1000; // 80 seconds

        // Cache Random
        private static readonly Random Random = new Random();

        #endregion

        #region Constructor

        public VillageShop(IList<IItem> items)
        {
            _stock = items;
        }

        public VillageShop()
        {
            _stock = GenerateStock();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Randomly changes shop popularity value.
        /// </summary>
        public void UpdatePopularity()
        {
            _currentPopularity = Random.Next(10, 100) / 100f;
            _popularityUpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// Calculates price for an item based on buyer's reputation.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <param name="buyerReputation"></param>
        /// <formula>
        /// Formula:
        ///  (priceMultiplier - (buyerReputation + (1 - shopMultiplier)) / 2) * price
        ///
        ///  * Price Multiplier - Max price multiplier,
        ///     So if this variable equals 1.2 and player has
        ///     reputation 0, then item will cost 20% more.
        ///
        ///  * buyerReputation - Buyer's reputation in the current shop (village).
        ///     Represented as a decimal value between 0 and 1.
        ///
        ///  * shopMultiplier (1 - shopMultiplier) - Village's sell price multiplier. Changes with time.
        ///    The value is different for every village and represented as a decimal value between 0 and 1.
        ///    The value should be 0, when the place is very popular, since when value equals 1 the price should be highest.
        ///    To achive this effect we divide the value with 1:
        ///         (1 - shopMultiplier)
        ///
        ///  * "/2" - Max possible sum of playerReputation and shopMultiplier.
        ///
        ///  * price - Item's real price
        /// 
        /// Example:
        ///     (1.2 - (.93 + (1 - .9)) / 2) * 80000 = 58800
        /// </formula>
        /// <returns>
        /// Optimal price
        /// </returns>
        private float CalculateItemPrice(int itemPrice, int buyerReputation)
        {
            if (DateTime.Now > _popularityUpdatedTime.AddMilliseconds(POPULARITY_REFRESH_TIME))
            {
                UpdatePopularity();
            }

            return (PRICE_MULTIPLIER - (buyerReputation + (1 - _currentPopularity)) / 2f) * itemPrice;
        }

        /// <summary>
        /// Regenerates stock state by randomizing some values,
        /// such as amount of different items, new items in the stock.
        /// </summary>
        public void RegenerateStock()
        {
            // Create a fast variable, in case if I will change the target.
            IList<IItem> stock = _stock;

            // Randomize item positions (just for a visible effect)
            stock.Shuffle();

            // Update last update time
            _lastStockUpdateTime = DateTime.Now;

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
                stock[ma].Amount = randomAmount;
            }

            // Add new items
            int newItemsCount = (Random.Next(0, 1) == 0) ? 0 : Random.Next(1, 4);

            for (var ma = 0; ma < newItemsCount; ++ma)
            {
                stock.Add(GenerateItem());
            }
        }

        /// <summary>
        /// Returns available items in the shop.
        /// </summary>
        /// <returns>
        /// Current items in the stock as List
        /// </returns>
        public IList<IItem> GetAvailableItems()
        {
            // Check if stock should be refreshed, by checking time.
            if (DateTime.Now > _lastStockUpdateTime.AddMilliseconds(STOCK_REFRESH_TIME))
            {
                RegenerateStock();
            }

            // Return stock state
            return _stock;
        }

        /// <summary>
        /// Generates (Randomizes) single item
        /// </summary>
        /// <returns>
        /// Generated item.
        /// </returns>
        private static IItem GenerateItem()
        {
            // Get all implementations of Item interface.
            var itemInterface = typeof(IItem);
            List<Type> classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(ma => itemInterface.IsAssignableFrom(ma) && ma.IsClass)
                .ToList();

            // Randomize item index
            int randomIndex = Random.Next(classes.Count);

            // Get type at that index
            Type itemType = classes.ElementAt(randomIndex);

            // Invoke type constructor
            var item = (IItem) Activator.CreateInstance(itemType);

            // Check if Activator was successfully able to invoke the constructor
            // If it couldn't invoke a constructor (in case if it exist), it will return NULL.
            if (item == null)
            {
                throw new ApplicationException(
                    $"Activator couldn't successfully invoke item's constructor: {itemType.Name}");
            }

            // Randomize amount
            int amount = Random.Next(12);

            // Update item's amount value
            item.Amount = amount;

            // Return the generated item
            return item;
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
            IList<IItem> randomizedItems = new List<IItem>();
            IList<IItem> stock = new List<IItem>();

            // Randomize array length
            int itemsCount = Random.Next(2, 8);

            // Generate items
            for (int ma = 0; ma < itemsCount; ++ma)
            {
                randomizedItems.Add(GenerateItem());
            }

            // Stack items (remove duplications and increase their amount value)
            foreach (IItem item in randomizedItems)
            {
                // Try find occurence
                IItem occurrence = stock.SingleOrDefault(io => item.Name == io.Name);
                ;

                // Check if an occurence found. Update the amount, but don't a new element (duplication) to the list
                if (occurrence != default)
                {
                    // Update known item amount
                    occurrence.UpdateAmount(1);
                    continue;
                }

                // Add a new item to the stock
                stock.Add(item);
            }

            // Return generted items
            return stock;
        }

        public IItem SellItem(IItem item)
        {
            // Remove item from the stock
            if (item.Amount - 1 == 0) RemoveItem(item); // remove item if amount will equal 0
            else item.UpdateAmount(-1); // reduce amount if it won't

            // Return item
            IItem soldItem = ObjectCopier.Clone(item);
            soldItem.Amount = 1;
            return soldItem;
        }

        /// <summary>
        /// Remove item from the stock
        /// (does not use amount value, but just deletes the instance from the stock)
        /// </summary>
        /// <returns>
        /// Boolean, that represents if this item was in the stock.
        /// </returns>
        /// <flags>
        /// * METHOD IS OPEN TO EXTENSION
        ///     Access modifer "private" can easily be changed to "public", since no code-links are present.
        /// </flags>
        private bool RemoveItem(IItem item)
        {
            int deletedElements = _stock.ToList().RemoveAll(ma => item == ma);

            return deletedElements > 0;
        }

        #endregion
    }
}