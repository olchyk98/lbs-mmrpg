using System;
using System.Collections.Generic;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.instances.player
{
    /// <summary>
    /// Canvas wrapper of player.
    /// </summary>
    public class DungeonPlayer : IObject2D
    {
        #region Fields

        public Player NativeEntity { get; }
        public int[] Position { get; set; }
        public Object2DRotation Rotation { get; set; } = Object2DRotation.RIGHT;
        public int MaxAttackCooldown { get; } = 4;
        public int AttackCooldown { get; set; } = 0;

        public readonly Dictionary<Object2DRotation, char> RotationModels = new Dictionary<Object2DRotation, char>()
        {
            {Object2DRotation.TOP, '▲'},
            {Object2DRotation.RIGHT, '►'},
            {Object2DRotation.BOTTOM, '▼'},
            {Object2DRotation.LEFT, '◄'}
        };

        #endregion
        
        #region Constructor
 
        public DungeonPlayer(Player player)
        {
            NativeEntity = player;
        }
        #endregion
        
        #region Methods

        /// <summary>
        /// Makes player send a shock wave that deals damage to all collided with the shakewave monsters
        /// </summary>
        public PlayerAttackLine Attack()
        {
            // Declare start position
            // X or Y value will be changed depend on player's rotation
            int[] startPosition = Position.DeepClone();
            
            // Declare end position
            // Copied from the start position, but will be changed during the calculation procedure
            int[] endPosition = startPosition.DeepClone();
            
            // Extract attack range
            int attackRange = NativeEntity.Stats.AttackRange;
            
            // Update start/end positions depend on player's rotation
            // It's important to remember than the system draws lines from left to right, from up to down.
            // Meaning left/up should be less than right/down.
            switch (Rotation)
            {
                case Object2DRotation.RIGHT:
                    ++startPosition[0];
                    endPosition[0] = startPosition[0];
                    endPosition[0] += attackRange;
                    break;
                case Object2DRotation.LEFT:
                    --startPosition[0];
                    endPosition[0] = startPosition[0];
                    startPosition[0] -= attackRange;
                    break;
                case Object2DRotation.TOP:
                    --startPosition[1];
                    endPosition[1] = startPosition[1];

                    startPosition[1] -= attackRange;
                    break;
                case Object2DRotation.BOTTOM:
                    startPosition[1]++;
                    endPosition[1] = startPosition[1];
                    
                    endPosition[1] += attackRange;
                    break;
                default:
                    throw new ApplicationException("Invalid rotation value passed!");
            }
            
            // Create an attack line
            PlayerAttackLine playerAttackLine = new PlayerAttackLine(
                startPosition, endPosition, 2, NativeEntity.Stats.AttackDamage);

            return playerAttackLine;
        }
        #endregion
    }
}