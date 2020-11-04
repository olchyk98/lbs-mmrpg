using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class UnknownSword : ISword
    {
        public int Amount { get; set; }
        public double Price { get; } = 2e6 * 1.3;
        public string Name { get; } = "Unknown Sword";
        public float Damage { get; } = 150;
        public int AttackRange { get; } = 8;
    }
}