using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class RainbowBoots : IBoots
    {
        public float CustomSpeed { get; } = 5f;
        public int Amount { get; set; }
        public string Name { get; } = "Rainbow Boots";
        public double Price { get; } = 1e5 * 9;
    }
}