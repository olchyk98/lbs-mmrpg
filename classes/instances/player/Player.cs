using System;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.contracts.entity;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.player
{
    // [Headhunter]
    public class Player : IEntity
    {
        #region Fields

        public float Health { get; private set; }

        public float MaxHealth { get; set; }
        public readonly PlayerStats Stats;
        public readonly PlayerVillage VillagesManager;
        public readonly PlayerMoney MoneyManager;
        public readonly PlayerInventory Inventory;

        // Reference to entity that the players is trying to kill right now
        private IEntity myCurrentTarget = default;

        #endregion

        #region Constructor

        public Player(Village village)
        {
            MaxHealth = Health = 100;
            MoneyManager = new PlayerMoney(this);
            Stats = new PlayerStats(this);
            VillagesManager = new PlayerVillage(this, village);
            Inventory = new PlayerInventory(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Internal method.
        /// Can be used to apply damage to the entity.
        /// Can be also used to heal the entity.
        /// </summary>
        /// <param name="health">
        /// A value that is not 0.
        ///
        /// You can heal the entity by passing a positive value,
        /// and apply damage to it by passing a negative value.
        /// </param>
        /// <returns>
        /// Boolean, that represents if player is still alive.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the damage value equals zero.
        /// </exception>
        private bool ChangeHealth(float health)
        {
            // Convert class to its interface to use some methods that are implemented in the interface
            IEntity instanceEntity = this;

            // Check if player is alive
            if (!instanceEntity.IsAlive())
            {
                throw new Exception("Tried to apply damage to a dead player!");
            }

            // Validate argument
            if (health == 0)
            {
                throw new ArgumentException("Health value cannot be zero!");
            }

            // Update health
            Health += health;

            // Keep the health value between 0 and MaxHealth
            if (Health < 0) Health = 0;
            else if (Health > MaxHealth) Health = MaxHealth;

            // Return Status
            return instanceEntity.IsAlive();
        }

        /// <summary>
        /// Applies damage to the entity.
        /// </summary>
        /// <param name="damage">
        /// Value that is higher than zero.
        /// </param>
        /// <returns>
        /// Boolean that represents if entity is still alive.
        /// </returns>
        public bool ApplyDamage(float damage)
        {
            if (damage <= 0)
            {
                throw new ArgumentException("Damage value should be higher than zero!");
            }

            // Make damage calculations
            float appliedDamage = damage * (1 - Stats.DefenseProcent);

            // Apply damage
            return ChangeHealth(-appliedDamage);
        }

        /// <summary>
        /// Heals the entity.
        /// </summary>
        /// <param name="health">
        /// Health points that will be added to the entity's health.
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public void HealHealth(float health)
        {
            if (health <= 0)
            {
                throw new ArgumentException("Health value should be higher than zero!");
            }

            // Apply healing
            ChangeHealth(health);
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
            return Stats.DefenseProcent.ToString("0.00");
        }

        /// <summary>
        /// Regenerate HP by sleeping
        /// </summary>
        /// <param name="iterations">
        /// Number of sleeping iterations (ticks)
        /// </param>
        public void GainSleep(int iterations = 1)
        {
            // Initialize random instance
            Random random = new Random();

            // Apply sleep iterations
            for (var ma = 0; ma < iterations; ++ma)
            {
                float regeneratedHealth = (float) random.NextDouble() * Stats.HealthRegeneration;
                HealHealth(iterations * regeneratedHealth);
            }
        }

        /// <summary>
        /// Reduces balance money and adds item to the inventory
        /// (or just increments amount of the existing item in inventory)
        /// </summary>
        /// <param name="item"></param>
        public void BuyItem(IItem item)
        {
            bool isCouldBuy = MoneyManager.TakeMoney(item.PriceForPlayer);
            if (!isCouldBuy) return;

            // Add item to the inventory
            Inventory.AddItem(item);

            // Add village reputation
            VillagesManager.AddReputation(ActionReputation.BuyShopItem.Reputation);
        }

        /// <summary>
        /// Increases balance money and removes item from the inventory
        /// (or just decrements amount of the existing item in inventory)
        /// </summary>
        public void SellItem(IItem item)
        {
            Inventory.RemoveItem(item);
            MoneyManager.IncreaseMoney(item.SellPriceForPlayer);
        }

        #endregion
    }
}