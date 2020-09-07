using System;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.gui.components.dungeonengine
{
    public partial class DungeonEngine
    {
        #region Fields
        private Player2D myPlayer;
        #endregion
        
        #region External Constructor

        private void InitializePlayer()
        {
            myPlayer = SpawnPlayer();
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
                CanvasSize[0] / 2,
                CanvasSize[1] / 2
            };

            // Instantiate player
            Player2D player = new Player2D(Program.Player);

            // Set player 2d position
            player.Position = position;

            // Return the created player instance
            return player;
        }

        /// <summary>
        /// Listens to keyboard, filter buttons and returns x and y movement directions
        /// </summary>
        /// <returns>
        /// Array of two-axis directions - [x, y]
        /// </returns>
        private int[] GetInputDirections()
        {
            // Declare direction array
            int[] direction = new int[2]; // [x,y]

            // Don't return anything till an valid input received
            while (true)
            {
                // Get keyboard input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Declare boolean that represents if ve got a valid input
                bool isValidInput = true;

                // Check which key is pressed
                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        direction[0] = -1;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        direction[0] = 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        direction[1] = -1;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        direction[1] = 1;
                        break;
                    default: // No valid input
                        isValidInput = false;
                        continue;
                }

                // Exit while loop if a valid input received
                if (isValidInput) break;
            }

            // Return direction array
            return direction;
        }

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
            const int borderSize = 1;
            
            // Check if player touches the border -> Return
            return x < borderSize || x > CanvasSize[0] - borderSize || y < borderSize || y > CanvasSize[1] - borderSize;
        }

        #endregion
    }
}