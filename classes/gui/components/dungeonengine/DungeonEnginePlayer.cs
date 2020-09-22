using System;
using System.Collections.Generic;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    public partial class DungeonEngine
    {
        #region Fields
        private DungeonPlayer myPlayer;
        private readonly IList<PlayerAttackLine> myPlayerAttackLines = new List<PlayerAttackLine>();
        #endregion
        
        #region External Constructor

        private void InitializePlayer()
        {
            myPlayer = SpawnPlayer();
        }
        #endregion
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Methods
        /// <summary>
        /// Creates a new player2D instance and places it in the middle of the screen.
        /// </summary>
        /// <returns>
        /// Created canvas wrapper of player
        /// </returns>
        private DungeonPlayer SpawnPlayer()
        {
            // Calculate position
            int[] position =
            {
                CanvasSize[0] / 2,
                CanvasSize[1] / 2
            };

            // Instantiate player
            DungeonPlayer dungeonPlayer = new DungeonPlayer(Program.Player);

            // Set player 2d position
            dungeonPlayer.Position = position;

            // Return the created player instance
            return dungeonPlayer;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Listens to keyboard, filter buttons and returns currently pressed button
        /// </summary>
        /// <returns>
        /// Pressed button value
        /// </returns>
        private DungeonInputEvent GetInputEvent()
        {
            // Don't return anything till an valid input received
            while (true)
            {
                // Get keyboard input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Check which key is pressed
                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        return DungeonInputEvent.MOVE_LEFT;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        return DungeonInputEvent.MOVE_RIGHT;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        return DungeonInputEvent.MOVE_UP;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        return DungeonInputEvent.MOVE_DOWN;
                    case ConsoleKey.Enter:
                        return DungeonInputEvent.ATTACK;
                    default: // No valid input
                        continue;
                }
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Moves player in the target direction.
        /// </summary>
        /// <param name="directionX">
        /// Movement direction in the X axis
        /// </param>
        /// <param name="directionY">
        /// Movement direction in the Y axis
        /// </param>
        /// <returns>
        /// Boolean that represents if player currently touching the canvas border
        /// </returns>
        private bool MovePlayer(int directionX, int directionY)
        {
            // Validate arguments
            if (Math.Abs(directionX) > 1 || Math.Abs(directionY) > 1)
            {
                throw new Exception($"Invalid direction. Value should be 1, -1 or 0. Got: {directionX}:{directionY}");
            }
            
            // Convert to its object2d interface, since it contains premade movement methods
            IObject2D objectPlayer = myPlayer;
            
            // Move the player
            objectPlayer.MoveDirection(directionX, directionY);
            
            // Check if player is touching the border -> Return result
            return CheckPlayerTouchesBorder();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Checks if player touches the canvas border
        /// </summary>
        /// <returns>
        /// Boolean that represents if player is currently touching the canvas border
        /// </returns>
        private bool CheckPlayerTouchesBorder()
        {
            // Extract position axis
            int x = myPlayer.Position[0];
            int y = myPlayer.Position[1];
            
            //
            int borderSize = CanvasBorderSize;
            
            // Check if player touches the border -> Return
            return x < borderSize || x > CanvasSize[0] - 1 - borderSize || y < borderSize || y > CanvasSize[1] - 1 - borderSize;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Creates a new attack line that comes from player and
        /// damages all nearby monsters.
        /// </summary>
        private void MakePlayerAttack()
        {
            // Don't process if player's attack is on delay
            if (myPlayer.AttackCooldown > 0) return;
            
            // Update player's attack delay
            myPlayer.AttackCooldown = myPlayer.MaxAttackCooldown;
            
            // Create attack line
            PlayerAttackLine attackLine = myPlayer.Attack();
                        
            // Add to the lines
            myPlayerAttackLines.Add(attackLine);
        }

        #endregion
    }
}