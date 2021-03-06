using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.items
{
    /// <summary>
    /// Better armor will help you survive monsters.
    /// The better armor you have the less damage you will get.
    /// </summary>
    public interface IArmor : IEquipable
    {
        public float ProtectionProcent { get; }
        
        /// <summary>
        /// Checks if item equiped (placed) on any inventory slots
        /// </summary>
        /// <param name="inventory">
        /// Player inventory that should be checked
        /// </param>
        /// <returns>
        /// Boolean that represents if item is equiped
        /// </returns>
        bool IEquipable.IsEquipedOn(PlayerInventory inventory)
        {
            return inventory.Armor?.Name == Name;
        }
        
        /// <summary>
        /// uses PlayerInventory as param to update target slot value
        /// </summary>
        /// <param name="inventory">
        /// Target inventory
        /// </param>
        void IEquipable.EquipOn(PlayerInventory inventory)
        {
            inventory.EquipArmor(this);
        }
        
        /// <summary>
        /// uses PlayerInventory as param to update target slot value
        /// </summary>
        /// <param name="inventory">
        /// Target inventory
        /// </param>
        void IEquipable.UnequipOn(PlayerInventory inventory)
        {
            inventory.EquipArmor(this, true);
        }
    }
}