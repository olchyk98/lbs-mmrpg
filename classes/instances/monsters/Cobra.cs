using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Cobra : IMonster
    {
        public int TicksPerMove { get; } = 5;
        public float Health { get; set; }
        public float MaxHealth { get; } = 100;
        public static string Name { get; } = "Cobra";
        public float HeadPrice { get; } = 16000;
        public float AttackDamage { get; } = 20.5f;
        public char ModelChar { get; } = '%';
        public int[] Position { get; set; }
    }
}