using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Cobra : AMonster
    {
        public override int TicksPerMove { get; } = 6;
        public static string FightDifficulty = "A+";
        public override float MaxHealth { get; } = 600;
        public static string Name { get; } = "Cobra";
        public override float HeadPrice { get; } = 16000;
        public override float AttackDamage { get; } = 20.5f;
        public override char ModelChar { get; } = '%';
        public override int[] Position { get; set; }
    }
}