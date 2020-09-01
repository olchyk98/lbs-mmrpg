using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Cobra : IMonster
    {
        public int TicksPerMove { get; } = 12;
        public float Health { get; set; }
        public float MaxHealth { get; } = 100;
        public static string Name { get; } = "Combra";
        public float HeadPrice { get; } = 16000;
    }
}