using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using lbs_rpg.classes.instances.player;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.entity;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    public partial class DungeonEngine
    {
        #region Fields
        private static readonly Random Random = new Random();

        private static readonly int[] CanvasSize = // x,y
        {
            20, 20
        };
        #endregion

        #region Constructor

        public DungeonEngine(Type monstersType)
        {
            InitializeMonsters(monstersType);
            InitializePlayer();
        }

        #endregion

        #region Tick Methods

        /// <summary>
        /// Starts the rendering/gaming process
        /// </summary>
        public void ProcessTicks()
        {
            // Keyboard cancellation variable
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            // Draw & Update
            // Run in a separate task to be able to handle input
            Task.Run(() =>
            {
                // Draw Recursion
                while (true)
                {
                    // Break if rendering process exited
                    if (token.IsCancellationRequested) break;
                    
                    // Update & Draw
                    bool didExit = UpdateTick();
                    DrawTick();

                    // Exit dungeon if player died or touched a wall
                    if (didExit)
                    {
                        tokenSource.Cancel();
                        break;
                    }
                }
            });

            // // Keyboard input
            while (true)
            {
                // Break if rendering process exited
                if (token.IsCancellationRequested) break;
            
                // Handle input
                int[] inputDirections = GetInputDirections();
            
                // Move player using those values
                bool isTouchingBorder = MovePlayer(inputDirections[0], inputDirections[1]);
                
                // Stop game if player touches the border
                if (isTouchingBorder)
                {
                    tokenSource.Cancel();
                    break;
                }
            }
        }


        /// <summary>
        /// Prints game entities and objects
        /// </summary>
        private void DrawTick()
        {
            // Initialize output chars array
            IList<char[]> outputBuilder = new List<char[]>();

            for (int ma = 0; ma < CanvasSize[1]; ++ma)
            {
                // Create char array & Fill the blanks (spaces)
                char[] charArray = new char[CanvasSize[0]].Select(_ => ' ').ToArray();
                
                // Push to the output array
                outputBuilder.Add(charArray);
            }

            // Draw monsters
            foreach (IMonster monster in myMonsters)
            {
                // Set cursor position
                int[] pos = monster.Position;

                // Print monster
                outputBuilder.ElementAt(pos[1])[pos[0]] = '%';
            }

            // Draw player
            outputBuilder.ElementAt(myPlayer.Position[1])[myPlayer.Position[0]] = '#';

            // Convert builder to output
            string[] outputArray = new string[outputBuilder.Count];
            
            for (var my = 0; my < outputBuilder.Count; ++my)
            {
                // Add to the output array (convert chars to string)
                outputArray[my] = new string(outputBuilder.ElementAt(my));
            }

            // Clear console
            FastGuiUtils.ClearConsole();
            
            // Output
            FastGuiUtils.PrintCenteredText(outputArray);
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

            // Check if it's time to spawn a new monster
            if (--myTicksToMonsterSpawn <= 0)
            {
                RefreshMonsterSpawnTimeout();
                SpawnMonster();
            }

            // Wait one millisecond between ticks
            Task.Delay(10).Wait();

            // Return success and proceed to the next tick
            return false;
        }

        #endregion
    }
}