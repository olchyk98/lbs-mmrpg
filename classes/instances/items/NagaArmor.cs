using lbs_rpg.contracts;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    public class NagaArmor : IArmor
    {
        public int Amount { get; set; }
        public string Name { get; } = "Naga Armor";
        public float ProtectionProcent { get; } = 50f;
        public double Price { get; } = 1600000;
    }
}