using System;
using System.Collections.Generic;
using System.Linq;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerVillage
    {
        #region Fields
        public Village CurrentVillage { get; private set; }
        public Player Player { get; }

        private readonly Dictionary<Village, int> _villagesReputation;
        #endregion

        #region Constructor
        public PlayerVillage(Player player, Village currentVillage)
        {
            CurrentVillage = currentVillage;
            _villagesReputation = new Dictionary<Village, int>();
            Player = player;
        }
        #endregion

        #region Methods
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
            return GetVillageReputation(CurrentVillage);
        }

        /// <summary>
        /// Returns player's reputation as procent (current/max)
        /// </summary>
        /// <returns>
        /// Float that represents the procent value
        /// </returns>
        public float GetCurrentVillageReputationAsProcent()
        {
            return GetVillageReputationAsProcent(CurrentVillage);
        }

        /// <summary>
        /// Checks if player has enough health to travel to another village.
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <param name="player">
        /// Target player
        /// </param>
        /// <returns>
        /// Boolean that represents if player can travel to the target village.
        /// </returns>
        public bool CanTravelTo(Village village, Player player)
        {
            return player.Health - player.VillagesManager.GetTripHealthRequirement(village) > 0;
        }

        /// <summary>
        /// Returns number of health points that will be taken during the trip to another village.
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <returns>
        /// Number of health points
        /// </returns>
        public float GetTripHealthRequirement(Village village)
        {
            return Convert.ToSingle(village.GetDistanceTo(CurrentVillage) *
                                    PlayerStats.HealthAmountTripPerKm);
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
        public IList<Village> GetNearestVillages(int villagesCount, bool onlyAllowed = true)
        {
            // Cache current village
            Village currentVillage = this.CurrentVillage;

            // Ref list of all villages
            IList<Village> allVillages = Program.Villages.Villages;

            // Sort villages by distance
            List<Village> sortedVillages = allVillages
                .OrderByDescending(ma => ma.GetDistanceTo(currentVillage))
                .ToList();

            // Remove current village
            // Always on the first position, so can use .Skip, but this is safer.
            sortedVillages.Remove(currentVillage);

            // Process only allowed villages (villages that player can go to and survive)
            if (onlyAllowed)
            {
                // Make a duplication of the sortedVillages to prevent memory bleeding
                IList<Village> sortedVillagesOld = sortedVillages.ToList();
                
                // Can be replaced with a LINQ expression (.Where)
                foreach (Village village in sortedVillagesOld)
                {
                    // Check if player can travel
                    if (!CanTravelTo(village, Player))
                    {
                        sortedVillages.Remove(village);
                    }
                }
            }

            // Return [villagesCount] villages
            return sortedVillages.Take(villagesCount).ToList();
        }

        /// <summary>
        /// Returns distance between player's current village and target village in ticks.
        /// </summary>
        /// <returns>
        /// Number of ticks of movement animation.
        /// </returns>
        public int GetDistanceToInTicks(Village targetVillage)
        {
            return (int) Math.Ceiling(CurrentVillage.GetDistanceTo(targetVillage) / Player.Stats.MovementSpeed);
        }

        public int GetVillageReputation(Village village)
        {
            bool hasReputation = _villagesReputation.TryGetValue(village, out int reputation);

            // Set default value to reputation if no reputation for this village present
            if (!hasReputation)
            {
                _villagesReputation[CurrentVillage] = reputation;
            }

            // Return reputation value
            return reputation;
        }

        public float GetVillageReputationAsProcent(Village village)
        {
            return GetVillageReputation(village) / village.MaxReputation * 100;
        }

        /// <summary>
        /// Returns player's reputation in current village as a label.
        /// </summary>
        /// <param name="village"></param>
        /// <returns>
        /// Label string:
        ///     Bad (0-30%)
        ///     Good (31%-90%)
        ///     Best (91%-100%)
        /// </returns>
        public string GetVillageReputationAsString(Village village)
        {
            // Get reputation in procents
            float reputationProcent = GetVillageReputationAsProcent(village);
            
            // Return the value
            if (reputationProcent <= 30f) return "Bad";
            if (reputationProcent <= 90f) return "Good";
            if (reputationProcent <= 100f) return "Best";
            return "Unknown";
        }
        #endregion
    }
}