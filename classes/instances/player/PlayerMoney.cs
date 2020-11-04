using System;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerMoney
    {
        #region Fields

        public double Money { get; private set; }
        public Player Player { get; }

        #endregion

        #region Constructor
        public PlayerMoney(Player player)
        {
            Money = 1e8;
            Player = player;
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Increases money value
        /// </summary>
        /// <param name="amount">
        /// Amount of money that should be added.
        /// </param>
        public void IncreaseMoney(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount value cannot be less than zero!");
            }
            
            // Update money value
            Money += amount;
        }
        
        /// <summary>
        /// Takes money from the balance
        /// </summary>
        /// <returns>
        /// Boolean, that represents if player has enough money and operation is successful.
        /// If player doesn't have enough money, then balance won't be changed and the method will return false.
        /// </returns>
        public bool TakeMoney(double amount)
        {
            // Check if player has enough money
            if (Money - amount < 0) return false;
            
            // Update money value
            Money -= amount;
            
            // Return success
            return true;
        }
        
        /// <summary>
        /// Check if player has enough money.
        /// </summary>
        /// <param name="price">
        /// Goal money
        /// </param>
        /// <returns>
        /// Returns a value that represents how much money do player need to afford the price.
        /// </returns>
        public double CanAfford(double price)
        {
            // balance: 1000, need: 250; 250-1000=-750
            double moneyDifference = price - Money;
            
            return (moneyDifference > 0) ? moneyDifference : 0;
        }
        #endregion
    }
}