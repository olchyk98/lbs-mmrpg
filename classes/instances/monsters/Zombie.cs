using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Zombie : IMonster
    {
        public int TicksPerMove { get; } = 8;
        public float Health { get; set; }
        public float MaxHealth { get; } = 30;
        public static string Name { get; } = "Zombie";
        public float HeadPrice { get; } = 1000;
    }
}