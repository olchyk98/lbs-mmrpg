using System;
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

            // Draw player attack lines
            // Make a duplication of the attack lines array to prevent an exception
            // since the object can be changed in async mode (by the input handler)
            foreach (PlayerAttackLine line in myPlayerAttackLines.ToList())
            {
                // iterate through all vertical positions
                for (var my = 0; my < line.Height; ++my)
                {
                    for (var mx = 0; mx <= line.Width; ++mx)
                    {
                        // Calculate current char position
                        int x = line.StartPosition[0] + mx;
                        int y = line.StartPosition[1] + my;
                        
                        // Skip char if it's of-screen-range
                        if (y > CanvasSize[1] - 1 || y < 0 || x > CanvasSize[0] - 1 || x < 0)
                        {
                            continue;
                        }
                        
                        // Push char to the output stack
                        outputBuilder.ElementAt(y)[x] = '◌';
                    }
                }
            }
            
            // Draw monsters
            foreach (AMonster monster in myMonsters)
            {
                // Set cursor position
                int[] pos = monster.Position;

                // Print monster
                outputBuilder.ElementAt(pos[1])[pos[0]] = monster.ModelChar;
            }
            
            // Draw player
            outputBuilder.ElementAt(myPlayer.Position[1])[myPlayer.Position[0]] = myPlayer.RotationModels[myPlayer.Rotation];

            // Draw canvas border
            // Calculate how many horizontal cells should be colored (player's attack delay)
            int filledBorderChars = (int) Math.Floor((double)
                myPlayer.AttackCooldown / myPlayer.MaxAttackCooldown * CanvasSize[0]); 
            
            // This solution does not override every cell in the array, therefore it can be securely in the end
            // of the rendering process pipeline
            for (var my = 0; my < CanvasSize[1]; ++my)
            {
                for (var mx = 0; mx < CanvasSize[0]; ++mx)
                {
                    // Check if print cursor is on the first/last line of the canvas
                    // if yes -> print the whole line of "*" ( two || )
                    // Otherwise if currently on the first/last character in the line -> Print "*" ( two || )
                    if (
                        my == 0 || my == CanvasSize[1] - 1
                                || mx == 0 || mx == CanvasSize[0] - 1
                    )
                    {
                        // Depends on player's attack delay
                        char fillChar = (mx < filledBorderChars) ? '-' : '*';
                        
                        outputBuilder.ElementAt(my)[mx] = fillChar;
                    }
                }
            }

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
        #endregion
    }
}