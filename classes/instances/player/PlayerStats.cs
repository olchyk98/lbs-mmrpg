namespace lbs_rpg.classes.instances.player
{
    /// <summary>
    /// This class (container) contains all player stats, but health.
    /// Since health/maxhealth is an interface-linked property.
    /// </summary>
    public class PlayerStats
    {
        #region Fields
        public Player Player { get; }
        
        // How many health points do player regenerate (per tick) when he's asleep.
        public float HealthRegeneration { get; } = .5f;
        
        public float AttackDamage => Player.Inventory.Sword?.Damage ?? 5f;
        
        // Length of player's attack ray
        public int AttackRange => Player.Inventory.Sword?.AttackRange ?? 1;
        
        // With how many procents player reduces applied to him damage
        public float DefenseProcent => Player.Inventory.Armor?.ProtectionProcent ?? 0f;
        
        // Number of animation ticks per km
        public float MovementSpeed => Player.Inventory.Boots?.CustomSpeed ?? 1f; // km
        
        // Constant float that represents how much health
        // does trip to another village take per meter.
        public const float HEALTH_AMOUNT_TRIP_PER_KM = 1.25f; // 100/80=1.25f // 80km per 100hp
        public const float HEALTH_AMOUNT_SOCIALIZATION_PER_TICK = .375f; // 60/160 (60 health per 160 ticks)
        #endregion
        
        #region Constructor
        public PlayerStats(Player player)
        {
            Player = player;
        }
        #endregion
    }
}