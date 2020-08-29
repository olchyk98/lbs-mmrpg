using System;
using System.Collections.Generic;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerVillage
    {
        public Village CurrentVillage { get; private set; }
        public Player Player { get; }

        private readonly Dictionary<Village, int> _villagesReputation;

        public PlayerVillage(Player player, Village currentVillage)
        {
            CurrentVillage = currentVillage;
            _villagesReputation = new Dictionary<Village, int>();
            Player = player;
        }

        /// <summary>
        /// Migrate to another village
        /// </summary>
        public void Migrate(Village village)
        {
            CurrentVillage = village;
        }

        /// <summary>
        /// Takes current village and changes reputation for that village in the player's memory (PlayerVillage)
        /// </summary>
        /// <param name="reputation">
        /// Number of reputation points that should be added
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throwed in reputation is less than zero
        /// </exception>
        public void AddCurrentVillageReputation(int reputation)
        {
            // Validate value
            if (reputation < 0)
            {
                throw new ArgumentException("Reputation value cannot be less than zero!");
            }

            // Normalize reputation
            int newReputation = Math.Clamp(GetCurrentVillageReputation() + reputation, 0, CurrentVillage.MaxReputation);

            // Update the reputation value for this village
            _villagesReputation[CurrentVillage] = newReputation;
        }

        /// <summary>
        /// Returns player's reputation in the current village
        /// </summary>
        public int GetCurrentVillageReputation()
        {
            bool hasReputation = _villagesReputation.TryGetValue(CurrentVillage, out int reputation);

            // Set default value to reputation if no reputation for this village present
            // REFACTOR: Move to a private method
            if (!hasReputation)
            {
                _villagesReputation[CurrentVillage] = reputation;
            }

            // Return reputation value
            return reputation;
        }

        /// <summary>
        /// Returns player's reputation as procent (current/max)
        /// </summary>
        /// <returns>
        /// Float that represents the procent value
        /// </returns>
        public float GetCurrentVillageReputationAsProcent()
        {
            return (float) GetCurrentVillageReputation() / CurrentVillage.MaxReputation * 100;
        }
    }
}