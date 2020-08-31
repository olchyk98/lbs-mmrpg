using System;
using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.items
{
    public interface IItem
    {
        #region
        // Float that represents procent of the price that the item should be sold for.
        // For example, real price is 800 and SELL_EXPONENT is 85% (.85). That means that item
        // will be sold for 800*.85=680$
        const float SellExponent = .65f; // 65% of its price

        public int Amount { get; set; }
        public double Price { get; }
        public double PriceForPlayer => GetPriceFor(Program.Player.VillagesManager);
        public double SellPrice => Price * SellExponent;
        public double SellPriceForPlayer => PriceForPlayer * SellExponent;
        public string Name { get; }
        #endregion
        
        /// <summary>
        /// Shortcut for Shop.CalculateItemPrice
        /// </summary>
        /// <param name="playerVillage">
        /// PlayerVillage instance
        /// </param>
        /// <returns>
        /// Price for specific player
        /// </returns>
        private double GetPriceFor(PlayerVillage playerVillage)
        {
            return playerVillage.CurrentVillage.Shop.CalculateItemPrice(Price,
                playerVillage.GetCurrentVillageReputationAsProcent() / 100);
        }

        /// <summary>
        /// Updates current amount value by [amount].
        /// Recommended values: 1 and -1.
        ///
        /// This method gives more control over code than default Increment/Decrement operators
        /// </summary>
        /// <param name="amount">
        /// Value that will be added to the current amount.
        ///
        /// Adding if positive and dividing if negative.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Casted if amount value is zero.
        /// </exception>
        /// <returns>
        /// Boolean, that represents if item's amount value does not equal zero after the update.
        /// </returns>
        public bool UpdateAmount(int amount)
        {
            // Check amount
            if (amount == 0)
            {
                throw new ArgumentException("Amount cannot equals zero.");
            }

            // Update value
            Amount += amount;
            
            // Return isfull
            return Amount != 0;
        }
    }
}