using lbs_rpg.contracts.entity;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.instances.player
{
    /// <summary>
    /// Canvas wrapper of player.
    /// </summary>
    public class Player2D : IObject2D
    {
        #region Fields

        public Player NativeEntity { get; }
        public int[] Position { get; set; }
        #endregion
        
        #region Constructor
 
        public Player2D(Player player)
        {
            NativeEntity = player;
        }
        #endregion
    }
}