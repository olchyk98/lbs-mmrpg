using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Spider : AMonster
    {
        public override int TicksPerMove { get; } = 4;
        public override float MaxHealth { get; } = 15;
        public static string FightDifficulty = "C";
        public static string Name { get; } = "Spider";
        public override float HeadPrice { get; } = 3000;
        public override float AttackDamage { get; } = 8.5f;
        public override char ModelChar { get; } = '@';
        public override int[] Position { get; set; }
    }
}