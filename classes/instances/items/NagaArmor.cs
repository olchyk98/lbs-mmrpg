using System;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class NagaArmor : IArmor
    {
        public int Amount { get; set; }
        public string Name { get; } = "Naga Armor";
        public float ProtectionProcent { get; } = 74f;
        public double Price { get; } = 1e6 * 1.5;
    }
}