using System;
using System.Collections.Generic;
using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    /// <summary>
    /// Partial class that is responsible for handling monsters list.
    /// </summary>
    public partial class DungeonEngine
    {
        #region Fields
        private readonly Type myMonstersType;
        private readonly List<IMonster> myMonsters;
        private int myTicksToMonsterSpawn;
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds a monster to the monsters list and places it
        /// on a random position
        /// </summary>
        private void SpawnMonster()
        {
            // Random monster position
            int multiplicatorX = Random.Next(0, 1);
            int multiplicatorY = Random.Next(0, 1);
            int[] position =
            {
                Console.WindowWidth * multiplicatorX,
                Console.WindowHeight * multiplicatorY
            };
            
            // Instantiate monster
            var monsterInstance = (IMonster) Activator.CreateInstance(myMonstersType);

            // Check if instantiated
            if (monsterInstance == null)
            {
                throw new ApplicationException($"Could not use activator to initialize an IMonster instance: {myMonstersType}");
            }
            
            // Set monster position
            monsterInstance.Position = position;
            
            // Add monster to the monsters list
            myMonsters.Add(monsterInstance);
        }

        /// <summary>
        /// Randomizes number of ticks (timeout) till the next monster will be spawned
        /// </summary>
        private void RefreshMonsterSpawnTimeout()
        {
            myTicksToMonsterSpawn = Random.Next(50, 400);
        }
        
        /// <summary>
        /// Removes target monster from the monsters list
        /// </summary>
        /// <param name="monster">
        /// Target monster
        /// </param>
        private void DestroyMonster(IMonster monster)
        {
            myMonsters.Remove(monster);
        }
        #endregion
    }
}