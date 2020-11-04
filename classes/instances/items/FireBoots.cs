using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class FireBoots : IBoots
    {
        public float CustomSpeed { get; } = 2f;
        public int Amount { get; set; }
        public string Name { get; } = "Fire Boots";
        public double Price { get; } = 48000;
    }
} 