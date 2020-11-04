using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class TravelBoots : IBoots
    {
        public float CustomSpeed { get; } = 5f;
        public int Amount { get; set; }
        public string Name { get; } = "Travel Boots";
        public double Price { get; } = 116000;
    }
}