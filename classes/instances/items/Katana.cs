using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class Katana : ISword
    {
        public int Amount { get; set; }
        public double Price { get; } = 16000;
        public string Name { get; } = "Katana";
        public float Damage { get; } = 15;
        public int AttackRange { get; } = 4;
    }
}