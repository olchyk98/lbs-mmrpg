using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.items
{
    public interface IUseable : IItem
    {
        public void UseOn(Player player);
    }
}