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

        private readonly Dictionary<Village, int> myVillagesReputation;

        #endregion

        #region Constructor

        public PlayerVillage(Player player, Village currentVillage)
        {
            CurrentVillage = currentVillage;
            myVillagesReputation = new Dictionary<Village, int>();
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
        /// <param name="amount">
        /// Number of reputation points that should be added
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throwed in reputation is less than zero
        /// </exception>
        public void AddReputation(int amount)
        {
            AddReputation(CurrentVillage, amount);
        }

        /// <summary>
        /// Takes target village and changes reputation for that village in the player's memory (PlayerVillage)
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <param name="amount">
        /// Number of reputation points that should be added
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throwed in reputation is less than zero
        /// </exception>
        public void AddReputation(Village village, int amount)
        {
            // Validate value
            if (amount < 0)
            {
                throw new ArgumentException("Reputation value cannot be less than zero!");
            }

            // Normalize reputation
            int newReputation = Math.Clamp(GetReputation() + amount, 0, village.MaxReputation);

            // Update the reputation value for this village
            myVillagesReputation[village] = newReputation;
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
        public bool CanTravelTo(Village village)
        {
            return Player.Health - Player.VillagesManager.GetTripHealthRequirement(village) > 0;
        }

        /// <summary>
        /// Checks if player has enough health to do a village task.
        /// </summary>
        /// <param name="task">
        /// Target task
        /// </param>
        /// <returns>
        /// Boolean that represents if player another health to complete the target task
        /// </returns>
        public bool CanDoTask(VillageTask task)
        {
            return Player.Health - GetTaskHealthRequirement(task) > 0;
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
                                    PlayerStats.HEALTH_AMOUNT_TRIP_PER_KM);
        }

        public float GetTaskHealthRequirement(VillageTask task)
        {
            return Convert.ToSingle(task.DurationTicks * PlayerStats.HEALTH_AMOUNT_SOCIALIZATION_PER_TICK);
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
                    if (!CanTravelTo(village))
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

        /// <summary>
        /// Returns player's reputation points for the target village.
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <returns>
        /// Reputation points
        /// </returns>
        public int GetReputation(Village village)
        {
            myVillagesReputation.TryGetValue(village, out int reputation);

            // Set default value to reputation if no reputation for this village present
            if (reputation == 0)
            {
                myVillagesReputation[village] = reputation;
            }

            // Return reputation value
            return reputation;
        }

        /// <summary>
        /// Returns player's reputation points in procents for the target village.
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <returns>
        /// Reputation rate (player's rate / max rate that player can get in the village)
        /// </returns>
        public float GetReputationAsProcent(Village village)
        {
            return GetReputation(village) / village.MaxReputation * 100;
        }

        /// <summary>
        /// Returns player's reputation in the target village as a label.
        /// </summary>
        /// <param name="village">
        /// Target village
        /// </param>
        /// <returns>
        /// Label string:
        ///     Bad (0-30%)
        ///     Good (31%-90%)
        ///     Best (91%-100%)
        /// </returns>
        public string GetReputationAsString(Village village)
        {
            // Get reputation in procents
            float reputationProcent = GetReputationAsProcent(village);

            // Return the value
            if (reputationProcent <= 30f) return "Bad";
            if (reputationProcent <= 90f) return "Good";
            if (reputationProcent <= 100f) return "Best";
            return "Unknown";
        }

        /// <summary>
        /// Returns player's reputation in the current village.
        ///
        /// More detailed information about this method you can get in the main overload.
        /// </summary>
        /// <returns>
        /// Reputation points
        /// </returns>
        public int GetReputation()
        {
            return GetReputation(CurrentVillage);
        }

        /// <summary>
        /// Returns player's reputation as procent (current/max)
        ///
        /// More detailed information about this method you can get in the main overload.
        /// </summary>
        /// <returns>
        /// Float that represents the procent value
        /// </returns>
        public float GetReputationAsProcent()
        {
            return GetReputationAsProcent(CurrentVillage);
        }

        /// <summary>
        /// Returns player's reputation as a label.
        ///
        /// More detailed information about this method you can get in the main overload.
        /// </summary>
        /// <returns>
        /// BAD, GOOD, BEST
        /// </returns>
        public string GetReputationAsString()
        {
            return GetReputationAsString(CurrentVillage);
        }

        #endregion
    }
}