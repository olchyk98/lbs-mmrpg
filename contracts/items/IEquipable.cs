using System;
using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.items
{
    public interface IEquipable : IItem
    {
        bool IsEquipedOn(PlayerInventory inventory)
        {
            throw new NotImplementedException("Item's interface does not implement IsEquipedOn method");
        }

        void EquipOn(PlayerInventory inventory)
        {
            throw new NotImplementedException("Item's interface does not implement EquipOn method");
        }
        
        void UnequipOn(PlayerInventory inventory)
        {
            throw new NotImplementedException("Item's interface does not implement UnequipOn method");
        }
    }
}