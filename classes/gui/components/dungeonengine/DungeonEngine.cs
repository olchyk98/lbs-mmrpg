using System;
using System.Threading;
using System.Threading.Tasks;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    public partial class DungeonEngine
    {
        #region Fields
        private static readonly Random Random = new Random();

        private static readonly int CanvasBorderSize = 1;

        private static readonly int[] CanvasSize = {80, 20}; // x, y

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Constructor
        public DungeonEngine(Type monstersType)
        {
            InitializeMonsters(monstersType);
            InitializePlayer();
        }

        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Methods

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
                        // Cancel the task
                        tokenSource.Cancel();
                        HandleExit();
                        break;
                    }

                    // Wait one millisecond between ticks
                    Task.Delay(80).Wait();
                }
            });

            // Keyboard input
            while (true)
            {
                // Break if rendering process exited
                if (token.IsCancellationRequested) break;

                // Handle input
                DungeonInputEvent inputEvent = GetInputEvent();
    
                // Declare movement direction vector
                int[] movementVector = {0, 0};
                    
                // Process input event
                switch (inputEvent)
                {
                    case DungeonInputEvent.MOVE_UP:
                        --movementVector[1];
                        break;
                    case DungeonInputEvent.MOVE_DOWN:
                        ++movementVector[1];
                        break;
                    case DungeonInputEvent.MOVE_LEFT:
                        --movementVector[0];
                        break;
                    case DungeonInputEvent.MOVE_RIGHT:
                        ++movementVector[0];
                        break;
                    case DungeonInputEvent.ATTACK:
                        MakePlayerAttack();
                        break;
                }
                bool isTouchingBorder = MovePlayer(movementVector[0], movementVector[1]);
                
                // Stop game if player touches the border
                if (isTouchingBorder)
                {
                    tokenSource.Cancel();
                    HandleExit();
                    break;
                }
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Changes values.
        /// Should be called exactly after the rp ends.
        /// </summary>
        private void HandleExit()
        {
            // Clear player's target
            myPlayer.NativeEntity.SetMonsterTarget(null);
        }
        #endregion
    }
}