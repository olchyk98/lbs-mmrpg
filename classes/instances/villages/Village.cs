using System;
using System.Collections.Generic;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.villages
{
    public class Village
    {
        public string Name { get; }
        public VillageShop Shop { get; }
        public int MaxReputation { get; }
        // Village position is represented as a X value (m)
        public double Position { get; }
        private const int MAX_REPUTATION_LIMIT = 4000;

        public Village(string name, double position)
        {
            Random random = new Random();
            
            Name = name;
            Shop = new VillageShop();
            MaxReputation = random.Next(100, MAX_REPUTATION_LIMIT);
            Position = position;
        }

        /// <summary>
        /// Returns distance between this village and target village in m.
        /// </summary>
        /// <param name="village">
        /// Target village.
        /// </param>
        /// <returns>
        /// Distance in m.
        /// </returns>
        public double GetDistanceTo(Village village)
        {
            return Math.Abs(village.Position - Position);
        }

        /// <summary>
        /// Returns distance between this village and target village in km.
        /// </summary>
        /// <param name="village">
        /// Target village.
        /// </param>
        /// <returns>
        /// Distance in km.
        /// </returns>
        public double GetDistanceToAsKm(Village village)
        {
            return GetDistanceTo(village) / 1000;
        }
    }
}