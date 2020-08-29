using System;
using System.Collections.Generic;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.villages
{
    public class Village
    {
        public string Name { get; }
        public VillageShop Shop { get; }
        public int MaxReputation { get; private set; }
        private const int MAX_REPUTATION_LIMIT = 4000;

        public Village(string name)
        {
            Random random = new Random();
            
            Name = name;
            Shop = new VillageShop();
            MaxReputation = random.Next(100, MAX_REPUTATION_LIMIT);
        }
    }
}