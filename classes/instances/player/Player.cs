using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.contracts;
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
        private IEntity _currentTarget = default;
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
            VillagesManager.AddCurrentVillageReputation(ActionReputation.BUY_SHOP_ITEM.Reputation);
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

        /// <summary>
        /// Returns nearest villages to the player
        /// </summary>
        /// <param name="villagesCount">
        /// Limiter for the output
        /// </param>
        /// <param name="onlyAllowed">
        /// Boolean that represents if function won't return the villages that
        /// player cannot visit yet, since it's too far.
        /// </param>
        /// <returns>
        /// List of the nearest villages
        /// </returns>
        public IList<Village> GetNearestVillages(int villagesCount, bool onlyAllowed = false)
        {
            // Cache current village
            Village currentVillage = VillagesManager.CurrentVillage;
            
            // Ref list of all villages
            IList<Village> allVillages = Program.Villages.Villages;
            
            // Sort villages by distance
            List<Village> sortedVillages = allVillages
                .OrderByDescending(ma => ma.GetDistanceTo(currentVillage))
                .ToList();
            
            // Remove current village
            // Always on the first position, so can use .Skip, but this is safer.
            sortedVillages.Remove(currentVillage);

            // Process only allowed villages
            if (onlyAllowed)
            {
                for (var ma = 0; ma < sortedVillages.Count; ++ma)
                {
                    // Access village
                    Village village = sortedVillages.ElementAt(ma);
                    
                    // Check if player can travel
                    if (!CanTravelTo(village))
                    {
                        sortedVillages.RemoveAt(ma);
                    }
                }
            }
            
            // Return [villagesCount] villages
            return sortedVillages.Take(villagesCount).ToList();
        }

        /// <summary>
        /// Checks if player has enough health to travel to another village.
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <returns>
        /// Boolean that represents if player can travel to the target village.
        /// </returns>
        public bool CanTravelTo(Village village)
        {
            return Health - village.GetDistanceTo(VillagesManager.CurrentVillage) *
                PlayerStats.HEALTH_AMOUNT_PER_TRAVEL > 0;
        }
        #endregion
    }
}