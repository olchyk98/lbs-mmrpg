using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.items
{
    /// <summary>
    /// Better weapon will allow you kill monsters faster.
    /// It also increases your attack shockwave range.
    /// </summary>
    public interface ISword : IEquipable
    {
        public float Damage { get; }
        public int AttackRange { get; }

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
            return inventory.Sword?.Name == Name;
        }
        
        /// <summary>
        /// uses PlayerInventory as param to update target slot value
        /// </summary>
        /// <param name="inventory">
        /// Target inventory
        /// </param>
        void IEquipable.EquipOn(PlayerInventory inventory)
        {
            inventory.EquipWeapon(this);
        }
        
        /// <summary>
        /// uses PlayerInventory as param to update target slot value
        /// </summary>
        /// <param name="inventory">
        /// Target inventory
        /// </param>
        void IEquipable.UnequipOn(PlayerInventory inventory)
        {
            inventory.EquipWeapon(this, true);
        }
    }
}