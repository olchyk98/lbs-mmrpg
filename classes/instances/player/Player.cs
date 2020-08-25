using System;
using lbs_rpg.contracts;

namespace lbs_rpg.classes.instances.player
{
    // [Headhunter]
    public class Player : IEntity
    {
        public float Health { get; private set; }

        public float MaxHealth { get; private set; }
        public float DefenseProcent { get; private set; }
        public float MoneyBalance { get; private set; }

        // Reference to entity that the players is trying to kill right now
        private IEntity _currentTarget = default;
        private PlayerHeadsStorage _headsStorage = new PlayerHeadsStorage();

        public Player()
        {
            MaxHealth = Health = 100;
            DefenseProcent = 0;
            MoneyBalance = 0;
        }

        /// <summary>
        /// Can be used to apply damage to the entity.
        /// Can be also used to heal the entity.
        /// </summary>
        /// <param name="damage">
        /// A value that is not 0.
        ///
        /// You can heal the entity by passing a negative value.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the damage value equals zero.
        /// </exception>
        public bool ApplyDamage(float damage)
        {
            // TODO: Check if dead

            // Validate argument
            if (damage == 0)
            {
                throw new ArgumentException("Damage value cannot be 0!");
            }

            // Make damage calculations
            float appliedDamage = damage * (1 - DefenseProcent);

            // Reduce health
            Health -= appliedDamage;

            // Keep the health value between 0 and MaxHealth
            if (Health < 0) Health = 0;
            else if (Health > MaxHealth) Health = MaxHealth;

            // Return Status
            return ((IEntity) this).IsAlive();
        }

        /// <summary>
        /// Converts Health value to procent form
        /// </summary>
        /// <returns></returns>
        public int GetHealthProcent()
        {
            return (int) Math.Floor(Health / MaxHealth * 100);
        }

        /// <summary>
        /// Converts health an outputable form 
        /// </summary>
        /// <returns></returns>
        public string HealthToString()
        {
            return $"{Health:0.0} / {MaxHealth:0.0}hp";
        }

        /// <summary>
        /// Converts DefenseProcent value to the outputable format.
        /// </summary>
        /// <returns></returns>
        public string GetDefenseProcentString()
        {
            return DefenseProcent.ToString("0.00");
        }

        /// <summary>
        /// Adds money to the player's balance.
        /// Method can be also used to take money from the player.
        /// </summary>
        /// <param name="amount">
        /// Takes money from the player if the value is negative.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Casts an ArgumentException if the amount value equals zero.
        /// </exception>
        public void AddMoney(int amount)
        {
            // Validate amount
            if (amount == 0)
            {
                throw new ArgumentException("Amount value cannot be 0!");
            }
            
            // Update the balance
            MoneyBalance += amount;
        }
    }
}