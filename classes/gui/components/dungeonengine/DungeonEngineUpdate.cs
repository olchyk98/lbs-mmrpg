using System.Collections.Generic;
using System.Linq;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    public partial class DungeonEngine
    {
        #region Sequence Methods
        /// <summary>
        /// Calculates new position for monsters and objects.
        /// </summary>
        /// <returns>
        /// Boolean that represents if game ends on this tick.
        /// Can be true if player is dead or if player touched a wall.
        /// </returns>
        private bool UpdateTick()
        {
            // Update monsters and return with status PLAYER_KILLED if function returns true
            if (UpdateMonsters()) return true;

            // Check if it's time to spawn a new monster
            UpdateMonsterSpawnTimer();
            
            // Update player's attack cd
            UpdatePlayerCooldown();

            // Return success and proceed to the next tick
            return false;
        }
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Partition Methods
        /// <summary>
        /// Updates monsters' position
        /// </summary>
        private bool UpdateMonsters()
        {
            // Keep track of monsters should be removed
            IList<AMonster> deadMonster = new List<AMonster>();
            
            // Updates monsters' position
            foreach (AMonster monster in myMonsters)
            {
                // Check if it's time to attack
                if (monster.IsTouchingObject(myPlayer))
                {
                    deadMonster.Add(monster);
                    bool isAlive = myPlayer.NativeEntity.ApplyDamage(monster.AttackDamage);

                    // Check if player is still alive
                    if (!isAlive)
                    {
                        // return exit status
                        return true;
                    }

                    // Move to the next monster
                    continue;
                }


                // Otheriwse, move to the player
                if (!DecrementMonstersMoveTimer(monster))
                {
                    // Move monster
                    monster.MoveToObject(myPlayer);

                    // Reset movement timer
                    myMonstersMovementTicks[monster] = monster.TicksPerMove;
                }
            }
            
            // Delete killed monsters
            foreach (AMonster monster in deadMonster)
            {
                RemoveMonster(monster);
            }
            
            // Update attack lines lifetime
            UpdateAttackLines();
            
            // Return PLAYER_NOT_KILLED status
            return false;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Updates monster spawner timer
        /// </summary>
        private void UpdateMonsterSpawnTimer()
        {
            // Decrease ticks number check if it's less
            // than zero -> Spawn a new monster
            if (--myTicksToMonsterSpawn <= 0)
            {
                RefreshMonsterSpawnTimeout();
                SpawnMonster();
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Checks for lines for collision with entities (monsters)
        /// </summary>
        private void UpdateAttackLines()
        {
            // Update array
            // Make a copy of the array, because we are modifing the array in the loop
            // that can cause memory leak (memory sequence rebuilding) | application crash
            foreach (PlayerAttackLine line in myPlayerAttackLines.ToList())
            {
                // Delete line if lifetime is zero
                if (--line.LifeTicksLeft <= 0)
                {
                    myPlayerAttackLines.Remove(line);
                    continue;
                }
                
                // Touch enemies
                // Create a copy since we will mofidy the list in the loop body
                foreach(AMonster monster in myMonsters.ToList())
                {
                    if (monster.Position[0] >= line.StartPosition[0] && monster.Position[0] <= line.EndPosition[0] &&
                        monster.Position[1] >= line.StartPosition[1] && monster.Position[1] <= line.EndPosition[1]
                    )
                    {
                        Program.Player.SetMonsterTarget(monster);
                        bool isAlive = line.AttackEntity(monster);
                        
                        // Remove monster if it's dead.
                        if (!isAlive)
                        {
                            // Give player money for the monster's head
                            myPlayer.NativeEntity.MoneyManager.IncreaseMoney(monster.HeadPrice);
                            
                            // Remove player from the canvas
                            RemoveMonster(monster);
                        }
                    }
                }
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Updates player's cooldown
        /// </summary>
        private void UpdatePlayerCooldown()
        {
            if (myPlayer.AttackCooldown > 0)
            {
                --myPlayer.AttackCooldown;
            }
        }
        #endregion
    }
}