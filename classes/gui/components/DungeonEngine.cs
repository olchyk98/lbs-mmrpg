using System;
using System.Collections.Generic;
using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.gui.components
{
    public class DungeonEngine
    {
        #region Fields

        private Type myMonstersType;
        private List<IMonster> myMonsters;
        #endregion
        
        #region Constructor
        public DungeonEngine()
        {
            
        }
        #endregion
        
        #region Methods

        /// <summary>
        /// Adds a monster to the monsters list and places it
        /// on a random position
        /// </summary>
        private void SpawnMonster()
        {
            // Instantiate monster
            var monsterInstance = (IMonster) Activator.CreateInstance(myMonstersType);
        }
        #endregion
        
        #region Tick Methods

        
        private void DrawTick()
        {
            
        }

        /// <summary>
        /// Calculates new position for monsters
        /// </summary>
        private void UpdateTick()
        {
            
        }
        #endregion
        
    }
}