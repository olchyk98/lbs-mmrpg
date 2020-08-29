using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    public class PermanentHealthBooster : IBuff
    {
        public int Amount { get; set; }
        public string Name { get; } = "Permanent Health Booster";
        public double Price { get; } = 120000;

        public void UseOn(Player player)
        {
            player.MaxHealth += 40;
        }
    }
}