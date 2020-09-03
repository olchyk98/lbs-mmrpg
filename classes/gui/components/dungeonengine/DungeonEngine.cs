using System;
using System.Collections.Generic;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.entity;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    public partial class DungeonEngine
    {
        #region Fields

        private readonly Player2D myPlayer;

        private static readonly Random Random = new Random();
        #endregion
        
        #region Constructor
        public DungeonEngine(Type monstersType)
        {
            myMonstersType = monstersType;
            myMonsters = new List<IMonster>();
            myPlayer = SpawnPlayer();

            RefreshMonsterSpawnTimeout();
        }
        #endregion
        
        #region Methods

        /// <summary>
        /// Creates a new player2D instance and places it in the middle of the screen.
        /// </summary>
        /// <returns>
        /// Created canvas wrapper of player
        /// </returns>
        private Player2D SpawnPlayer()
        {
            // Calculate position
            int[] position =
            {
                Console.WindowWidth / 2,
                Console.WindowHeight / 2
            };
            
            // Instantiate player
            Player2D player = new Player2D(Program.Player);
            
            // Set player 2d position
            player.Position = position;
            
            // Return the created player instance
            return player;
        }

        #endregion
        
        #region Tick Methods

        /// <summary>
        /// Starts the rendering/gaming process
        /// </summary>
        public void ProcessTicks()
        {
            // Draw Recursion
            while (true)
            {
                bool didExit = UpdateTick();
                DrawTick();

                // Exit dungeon if player died or touched a wall
                if (didExit) break;
            }
        }

        
        /// <summary>
        /// Prints game entities and objects
        /// </summary>
        private void DrawTick()
        {
            // Draw monsters
            foreach (IMonster monster in myMonsters)
            {
                // Set cursor position
                int[] pos = monster.Position;
                Console.SetCursorPosition(pos[0], pos[1]);
                
                // Print monster
                Console.WriteLine('%');
            }
            
            // Draw player
            Console.SetCursorPosition(myPlayer.Position[0], myPlayer.Position[1]);
            Console.WriteLine('#');
        }

        /// <summary>
        /// Calculates new position for monsters and objects.
        /// </summary>
        /// <returns>
        /// Boolean that represents if game ends on this tick.
        /// Can be true if player is dead or if player touched a wall.
        /// </returns>
        private bool UpdateTick()
        {
            // Updates monsters' position
            foreach (IMonster monster in myMonsters)
            {
                // Check if it's time to attack
                if (monster.IsTouchingObject(myPlayer))
                {
                    DestroyMonster(monster);
                    bool isAlive = myPlayer.NativeEntity.ApplyDamage(monster.AttackDamage);
                    
                    // Check if player is still alive
                    if (!isAlive)
                    {
                        // return exit status
                        return false;
                    }
                    
                    // Move to the next monster
                    continue;
                }
                
                
                // Otheriwse, move to the player
                monster.MoveToObject(myPlayer);
            }
            
            // Check if it's time to spawn a new monster
            if (--myTicksToMonsterSpawn <= 0)
            {
                RefreshMonsterSpawnTimeout();
                SpawnMonster();
            }

            // Return success and proceed to the next tick
            return true;
        }
        #endregion
        
    }
}