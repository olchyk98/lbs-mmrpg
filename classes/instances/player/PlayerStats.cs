using System;

namespace lbs_rpg.classes.instances.player
{
    /// <summary>
    /// This class (container) contains all player stats, but health.
    /// Since health/maxhealth is an interface-linked property.
    /// </summary>
    public class PlayerStats
    {
        public float HealthRegeneration { get; private set; }
        public float Level { get; private set; }
        public Player Player { get; }
        public float DefenseProcent => Player.Inventory.Armor?.ProtectionProcent ?? 0f;

        public PlayerStats(Player player)
        {
            HealthRegeneration = .05f;
            Level = 1;
            Player = player;
        }

        public void IncreaseLevel()
        {
            throw new NotImplementedException();
        }
    }
}