using System;

namespace lbs_rpg.classes.instances.player
{
    /// <summary>
    /// This class (container) contains all player stats, but health.
    /// Since health/maxhealth is an interface-linked property.
    /// </summary>
    public class PlayerStats
    {
        public float Level { get; private set; }
        public Player Player { get; }
        public float HealthRegeneration => Player.Inventory.Boots?.CustomSpeed ?? .5f;
        public float DefenseProcent => Player.Inventory.Armor?.ProtectionProcent ?? 0f;
        
        // Constant float that represents how much health
        // does trip to another village take per meter.
        public const float HEALTH_AMOUNT_PER_TRAVEL = 2;

        public PlayerStats(Player player)
        {
            Level = 1;
            Player = player;
        }

        public void IncreaseLevel()
        {
            throw new NotImplementedException();
        }
    }
}