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

        // Reference to entity that the players is trying to kill right now
        private IEntity _currentTarget = default;
        private PlayerHeadsStorage _headsStorage = new PlayerHeadsStorage(); 

        public Player()
        {
            MaxHealth = Health = 100;
            DefenseProcent = 0;
        }
        
        public bool ApplyDamage(float damage)
        {
            return false;
        }
        
        //// Getters that are easier to manage with regular methods
        /// <summary>
        /// Converts Health value to procent form, and covers to the outputable format.
        /// </summary>
        /// <returns></returns>
        public string GetHealthProcentString()
        {
            return Math.Floor(Health / MaxHealth * 100) + "%";
        }

        /// <summary>
        /// Converts DefenseProcent value to the outputable format.
        /// </summary>
        /// <returns></returns>
        public string GetDefenseProcentString()
        {
            return DefenseProcent.ToString("0.00");
        }
    }
}