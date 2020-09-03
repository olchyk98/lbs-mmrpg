using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Onyx : IMonster
    {
        public int TicksPerMove { get; } = 4;
        public float Health { get; set; }
        public float MaxHealth { get; } = 30;
        public static string Name { get; } = "Onyx";
        public float HeadPrice { get; } = 8000;
        public float AttackDamage { get; } = 30f;
        public int[] Position { get; set; }
    }
}