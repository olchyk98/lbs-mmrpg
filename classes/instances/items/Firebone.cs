using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class Firebone : ISword
    {
        public int Amount { get; set; }
        public double Price { get; } = 148000;
        public string Name { get; } = "Firebone";
        public float Damage { get; } = 50;
        public int AttackRange { get; } = 2;
    }
}