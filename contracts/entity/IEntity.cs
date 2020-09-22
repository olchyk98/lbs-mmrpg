using System;

namespace lbs_rpg.contracts.entity
{
    public interface IEntity
    {
        public float Health { get; set; }
        public float MaxHealth { get; }

        /// <summary>
        /// Applies damage to the entity
        /// </summary>
        /// <param name="damage">
        /// Damage
        /// </param>
        /// <returns>
        /// Boolean that represents if entity is still alive
        /// </returns>
        public bool ApplyDamage(float damage)
        {
            if (damage <= 0)
            {
                throw new ArgumentException("Damage value should be higher than zero!");
            }

            // Update health
            Health -= damage;
            
            // Returrn if entity is still alive
            return Health > 0;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}