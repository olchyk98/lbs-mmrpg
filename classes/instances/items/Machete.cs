using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class Machete : ISword
    {
        public int Amount { get; set; }
        public double Price { get; } = 250000;
        public string Name { get; } = "Machete";
        public float Damage { get; } = 50;
        public int AttackRange { get; } = 5;
    }
}