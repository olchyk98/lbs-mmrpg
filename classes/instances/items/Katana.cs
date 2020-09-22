using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class Katana : ISword
    {
        public int Amount { get; set; }
        public double Price { get; } = 4000;
        public string Name { get; } = "Fire Katana";
        public float Damage { get; } = 40;
        public int AttackRange { get; } = 30;
    }
}