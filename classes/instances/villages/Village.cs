using System;

namespace lbs_rpg.classes.instances.villages
{
    public class Village
    {
        public string Name { get; }
        public VillageShop Shop { get; }
        public VillageTasks Tasks { get; }

        public int MaxReputation { get; }
        // Village position is represented as a X value (km)
        public double Position { get; }
        private const int MAX_REPUTATION_LIMIT = 4000;

        public Village(string name, double position)
        {
            Random random = new Random();
            
            Name = name;
            Shop = new VillageShop();
            Tasks = new VillageTasks(this);
            MaxReputation = random.Next(100, MAX_REPUTATION_LIMIT);
            Position = position;
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
        public double GetDistanceTo(Village village)
        {
            return Math.Abs(village.Position - Position);
        }
    }
}