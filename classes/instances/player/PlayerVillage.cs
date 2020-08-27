using System.Collections.Generic;
using lbs_rpg.classes.instances.villages;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerVillage
    {
        public Village CurrentVillage { get; private set; }
        
        private readonly Dictionary<Village, int> _villagesReputation;

        public PlayerVillage(Village currentVillage)
        {
            CurrentVillage = currentVillage;
            _villagesReputation = new Dictionary<Village, int>();
        }
        
        /// <summary>
        /// Migrate to another village
        /// </summary>
        public void Migrate(Village village)
        {
            CurrentVillage = village;
        }
    }
}