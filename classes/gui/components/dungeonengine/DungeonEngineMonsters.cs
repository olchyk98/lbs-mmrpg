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
        private readonly List<AMonster> myMonsters = new List<AMonster>();
        private Type myMonstersType;
        private int myTicksToMonsterSpawn;
        private readonly IDictionary<AMonster, int> myMonstersMovementTicks = new Dictionary<AMonster, int>();

        #endregion
        
        #region External Constructor

        /// <summary>
        /// Part of the main constructor that initializes monster fields/timeouts
        /// </summary>
        private void InitializeMonsters(Type monstersType)
        {
            myMonstersType = monstersType;
            RefreshMonsterSpawnTimeout();
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds a monster to the monsters list and places it
        /// on a random position
        /// </summary>
        private void SpawnMonster()
        {
            // Random monster position
            int multiplicatorX = Random.Next(2); // 0, 1
            int multiplicatorY = Random.Next(2); // 0, 1
            int[] position =
            {
                (CanvasSize[0] - 1) * multiplicatorX,
                (CanvasSize[1] - 1) * multiplicatorY
            };
            
            // Instantiate monster
            var monsterInstance = (AMonster) Activator.CreateInstance(myMonstersType);

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
            myTicksToMonsterSpawn = Random.Next(10, 40);
        }
        
        /// <summary>
        /// Removes target monster from the monsters list
        /// </summary>
        /// <param name="monster">
        /// Target monster
        /// </param>
        private void RemoveMonster(AMonster monster)
        {
            myMonsters.Remove(monster);
        }

        /// <summary>
        /// Updates move timer for the target monster.
        /// </summary>
        /// <param name="monster">
        /// Target monster
        /// </param>
        /// <returns>
        /// Boolean that represents if timer hasn't ended yet
        /// </returns>
        private bool DecrementMonstersMoveTimer(AMonster monster)
        {
            // Check if already started tracking spawn time for this monster
            // (check if this key is in the dictionary)
            if (!myMonstersMovementTicks.ContainsKey(monster))
            {
                // Plus one, since we will decrement this value
                myMonstersMovementTicks[monster] = monster.TicksPerMove + 1;
            }

            // Check if timer ended and monster can move now
            bool isEnded = --myMonstersMovementTicks[monster] <= 0;
            
            // Reset timer if timer is ended
            if (isEnded)
            {
                myMonstersMovementTicks[monster] = monster.TicksPerMove;
            }
            
            // Return is not ended status
            return !isEnded;
        }
        #endregion
    }
}