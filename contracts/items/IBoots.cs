using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.items
{
    /// <summary>
    /// Better boots increases your speed, so that you can travel between villages faster
    /// </summary>
    public interface IBoots : IEquipable
    {
        public float SpeedProcent { get; }
        
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
            return inventory.Boots?.Name == Name;
        }
        
        /// <summary>
        /// uses PlayerInventory as param to update target slot value
        /// </summary>
        /// <param name="inventory">
        /// Target inventory
        /// </param>
        void IEquipable.EquipOn(PlayerInventory inventory)
        {
            inventory.EquipBoots(this);
        }
        
        /// <summary>
        /// uses PlayerInventory as param to update target slot value
        /// </summary>
        /// <param name="inventory">
        /// Target inventory
        /// </param>
        void IEquipable.UnequipOn(PlayerInventory inventory)
        {
            inventory.EquipBoots(this, true);
        }
    }
}