using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class LaserSword : ISword
    {
        public int Amount { get; set; }
        public double Price { get; } = 4e6;
        public string Name { get; } = "Laser Sword";
        public float Damage { get; } = 500;
        public int AttackRange { get; } = 5;
    }
}