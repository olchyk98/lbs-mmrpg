using System;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.items
{
    [Serializable]
    public class SoftHealthBooster : IUseable
    {
        public int Amount { get; set; }
        public string Name { get; } = "Soft Permanent Health Booster";
        public double Price { get; } = 120000;

        public void UseOn(Player player)
        {
            player.MaxHealth += 20;
        }
    }
}