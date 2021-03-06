using System;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class CrazyHealthBooster : IUseable
    {
        public int Amount { get; set; }
        public string Name { get; } = "Crazy Permanent Health Booster";
        public double Price { get; } = 8e6;

        public void UseOn(Player player)
        {
            player.MaxHealth += 200;
        }
    }
}