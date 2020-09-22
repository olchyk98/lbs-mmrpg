using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.monsters
{
    public class Zombie : AMonster
    {
        public override int TicksPerMove { get; } = 8;
        public override float MaxHealth { get; } = 30;
        public static string FightDifficulty = "D";
        public static string Name { get; } = "Zombie";
        public override float HeadPrice { get; } = 400;
        public override float AttackDamage { get; } = 5.6f;
        public override char ModelChar { get; } = '!';
        public override int[] Position { get; set; }
    }
}