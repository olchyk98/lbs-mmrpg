using lbs_rpg.contracts;

namespace lbs_rpg.classes.instances.monsters
{
    public class Monster : IEntityBot
    {
        public MonsterType Type;
        public float Health { get; private set; }
        public float MaxHealth { get; private set; }
        public int TicksPerMove { get; }

        public Monster(MonsterType type)
        {
            // Set Health values
            Health = MaxHealth = type.DefaultHealth;
            
            // Set movement values
            TicksPerMove = type.TicksPerMove;
            
            // Set type
            Type = type;
        }

        public bool ApplyDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}