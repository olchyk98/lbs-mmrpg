using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Onyx : AMonster
    {
        public override int TicksPerMove { get; } = 4;
        public static string FightDifficulty = "A-";
        public override float MaxHealth { get; } = 30;
        public static string Name { get; } = "Onyx";
        public override float HeadPrice { get; } = 8000;
        public override float AttackDamage { get; } = 30f;
        public override char ModelChar { get; } = '&';

        public override int[] Position { get; set; }
    }
}