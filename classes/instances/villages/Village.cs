using System.Collections.Generic;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.villages
{
    public class Village
    {
        public string Name { get; private set; }
        public VillageShop Shop { get; private set; }

        public Village(string name)
        {
            Name = name;
            Shop = new VillageShop();
        }
    }
}