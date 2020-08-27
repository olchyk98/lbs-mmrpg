namespace lbs_rpg.classes.instances.villages
{
    public class Village
    {
        public VillageShop Shop { get; private set; }
        public string Name { get; private set; }

        public Village(string name, VillageShop shop)
        {
            Shop = shop;
            Name = name;
        }
    }
}