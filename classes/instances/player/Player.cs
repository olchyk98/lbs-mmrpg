using System;
using lbs_rpg.classes.instances.villages;
using lbs_rpg.contracts;
using lbs_rpg.contracts.entity;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.player
{
    // [Headhunter]
    public class Player : IEntity
    {
        public float Health { get; private set; }

        public float MaxHealth { get; private set; }
        public float MoneyBalance { get; private set; }
        public readonly PlayerProperties Properties;
        public readonly PlayerVillage VillagesManager;
        public readonly PlayerInventory Inventory;
        private PlayerProperties _headsStorage = new PlayerProperties();

        // Reference to entity that the players is trying to kill right now
        private IEntity _currentTarget = default;

        public Player(Village village)
        {
            MaxHealth = Health = 100;
            MoneyBalance = 0;
            Properties = new PlayerProperties();
            VillagesManager = new PlayerVillage(village);
            Inventory = new PlayerInventory();
        }

        /// <summary>
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
        /// Takes money from the balance
        /// </summary>
        /// <returns>
        /// Boolean, that represents if player has enough money.
        /// If player doesn't have enough money, then balance won't be changed and the method will return false.
        /// </returns>
        private bool ReduceMoney(float amount)
        {
            // Check if player has enough money
            if (MoneyBalance - amount < 0) return false;
            
            // Update money value
            MoneyBalance -= amount;
            
            // Return success
            return true;
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
            float appliedDamage = damage * (1 - Properties.DefenseProcent);
            
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
            return Properties.DefenseProcent.ToString("0.00");
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
                throw new ArgumentException("Amount value cannot be zero!");
            }
            
            // Update the balance
            MoneyBalance += amount;
        }

        /// <summary>
        /// Regenerate HP by sleeping
        /// </summary>
        /// <param name="iterations">
        /// Number of sleeping iterations (ticks)
        /// </param>
        public void GainSleep(int iterations = 1)
        {
            HealHealth(iterations * Properties.HealthRegeneration);
        }

        /// <summary>
        /// Reduces balance money and adds item to the inventory
        /// </summary>
        /// <param name="item"></param>
        public void BuyItem(IItem item)
        {
            bool isCouldBuy = ReduceMoney(item.Price);
            if (!isCouldBuy) return;
            
            Inventory.AddItem(item);
        }
    }
}