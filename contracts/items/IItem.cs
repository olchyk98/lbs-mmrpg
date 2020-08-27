using System;

namespace lbs_rpg.contracts.items
{
    public interface IItem 
    {
        public int Amount { get; set; }
        public float Price { get; }
        public string Name { get; }

        /// <summary>
        /// Updates current amount value by [amount].
        /// Recommended values: 1 and -1.
        /// </summary>
        /// <param name="amount">
        /// Value that will be added to the current amount.
        ///
        /// Adding if positive and dividing if negative.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Casted if amount value is zero.
        /// </exception>
        public void UpdateAmount(int amount)
        {
            if (amount == 0)
            {
                throw new ArgumentException("Amount cannot equals zero.");
            }

            Amount += amount;
        }
    }
}