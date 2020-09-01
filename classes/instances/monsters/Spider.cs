using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Spider : IMonster
    {
        public int TicksPerMove { get; } = 4;
        public float Health { get; set; }
        public float MaxHealth { get; } = 30;
        public static string Name { get; } = "Spider";
        public float HeadPrice { get; } = 3000;
    }
}