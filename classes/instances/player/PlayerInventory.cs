using System.Collections.Generic;
using System.Linq;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerInventory
    {
        #region Fields
        private readonly IList<IItem> _items = new List<IItem>();
        #endregion

        #region Methods
        public void AddItem(IItem item)
        {
            // Check if player already have an item of that type (with that name)
            IItem similarItem = _items.SingleOrDefault(io => io.Name == item.Name);
            
            // Increase amount if already have this item
            if (similarItem != default)
            {
                similarItem.UpdateAmount(1);
                return;
            }
            
            // Add the item, ff no similar item in the inventory
            _items.Add(item);
        }
        #endregion
    }
}