using System;
using System.Collections.Generic;
using System.Linq;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerInventory
    {
        #region Fields

        public IList<IItem> Items { get; }

        public IArmor Armor { get; private set; }
        public IWeapon Weapon { get; private set; }
        public IBoots Boots { get; private set; }
        public Player Player { get; }

        #endregion

        #region Constructor

        public PlayerInventory(Player player)
        {
            Player = player;
            Items = new List<IItem>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds item to the inventory.
        /// Checks if item of that type is already in the inventory, and increments
        /// the item instance's amount if so.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IItem item)
        {
            // Check if player already have an item of that type (with that name)
            IItem similarItem = Items.SingleOrDefault(io => io.Name == item.Name);

            // Increase amount if already have this item
            if (similarItem != default)
            {
                similarItem.UpdateAmount(1);
                return;
            }

            // Add the item, ff no similar item in the inventory
            Items.Add(item);
        }

        /// <summary>
        /// Removes item from the inventory.
        /// Controls if there's an item of this type in the inventory and just changes its amount if so.
        /// </summary>
        /// <param name="item">
        /// Item that should be removed
        /// </param>
        /// <param name="ignoreAmount">
        /// If true the amount won't be decremeneted, but the item instance will be removed from the inventory
        /// </param>
        /// <exception cref="ApplicationException">
        /// Throwed if tried to remove item that is not in the inventory.
        /// </exception>
        public void RemoveItem(IItem item, bool ignoreAmount = false)
        {
            // Try to get the same item from the inventory
            IItem targetItem = Items.SingleOrDefault(io => io.Name == item.Name);

            // Check if inventory contains the item
            if (targetItem == default)
            {
                throw new ApplicationException("Tried to remove item that is not in the inventory");
            }

            // Remove item from the list
            bool doFullyRemove = ignoreAmount; // represents if the whole item instance should be removed

            // Decrement amount and check if the value equals 0 
            if (!ignoreAmount)
            {
                // doFullyRemove = !isNotEmpty
                doFullyRemove = !targetItem.UpdateAmount(-1);
            }

            // Remove item from the array if amount is zero or the ignoreAmount is true
            if (doFullyRemove)
            {
                if (item is IEquipable equipableItem)
                {
                    // Unequip equipable
                    equipableItem.UnequipOn(this);
                }

                // Remove from the inventory
                Items.Remove(targetItem);
            }
        }

        /// <summary>
        /// Equips target armor
        /// </summary>
        /// <devnote>
        /// Custom equipment setter.
        /// Created to have more control on what's going on.
        /// </devnote>
        public void EquipArmor(IArmor armor, bool unequip = false)
        {
            if (!unequip)
            {
                Armor = armor;
            } else if (Armor?.Name == armor.Name)
            {
                Armor = default;
            }
        }
        
        /// <summary>
        /// Equips target armor
        /// </summary>
        /// <devnote>
        /// Custom equipment setter.
        /// Created to have more control on what's going on.
        /// </devnote>
        public void EquipWeapon(IWeapon weapon, bool unequip = false)
        {
            if (!unequip)
            {
                Weapon = weapon;
            } else if (Weapon?.Name == weapon.Name)
            {
                Weapon = default;
            }
        }
        
        /// <summary>
        /// Equips target boots
        /// </summary>
        /// <devnote>
        /// Custom equipment setter.
        /// Created to have more control on what's going on.
        /// </devnote>
        public void EquipBoots(IBoots boots, bool unequip = false)
        {
            if (!unequip)
            {
                Boots = boots;
            } else if (Boots?.Name == boots.Name)
            {
                Boots = default;
            }
        }
        
        #endregion
    }
}