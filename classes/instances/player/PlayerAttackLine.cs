using System;
using System.Collections.Generic;
using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerAttackLine
    {
        #region Fields
        // The idea with two positions is to allow player change his attack range
        public int[] StartPosition { get; }
        public int[] EndPosition { get; }
        public int LifeTicksLeft { get; set; }
        public int Height => Math.Abs(EndPosition[1] - StartPosition[1]) + 1; // + 1 since height cannot be zero
        public int Width => Math.Abs(StartPosition[0] - EndPosition[0]);

        private readonly float myDamage;
        
        // List of entities that the line damage 
        private readonly IList<IEntity> myDamagedEntities = new List<IEntity>();

        #endregion
        
        #region Constructor

        public PlayerAttackLine(int[] startPosition, int[] endPosition, int ticksAlive, float damage)
        {
            LifeTicksLeft = ticksAlive;
            StartPosition = startPosition;
            EndPosition = endPosition;
            myDamage = damage;
        }
        #endregion
        
        #region Methods

        /// <summary>
        /// Checks if ray already touched target entity, and if no then
        /// damage will be applied to the target.
        /// </summary>
        /// <returns>
        /// Boolean that represents if entity is still alive.
        /// </returns>
        public bool AttackEntity(IEntity target)
        {
            // Don't attack the target twice
            if (myDamagedEntities.Contains(target)) return false;
            
            // Apply damage to target
            bool isAlive = target.ApplyDamage(myDamage);
            
            // Remember attacked entity
            myDamagedEntities.Add(target);

            // Retunr if dead status
            return isAlive;
        }
        #endregion
    }
}