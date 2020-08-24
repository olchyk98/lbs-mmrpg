using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using lbs_mrpg.classes.gui.components;

namespace lbs_mmrpg.classes.gui.components
{
    public static class FastGuiUtils
    {
        /// <summary>
        /// Determines how big container does array need,
        /// and centers (prints) all the array items.
        /// </summary>
        /// <param name="text">
        /// May be colorized using ANSI characters.
        ///
        /// NOTICE: This project has its own built-in class called Colorize. Use it to change the string style.
        /// </param>
        public static void PrintCenteredText(string[] text)
        {
            int textLines = text.Length;
            int[] windowDimensions = { Console.WindowWidth, Console.WindowHeight };

            // Iterate through all characters in the array.
            for (var ma = 0; ma < text.Length; ++ma)
            {
                // TODO [completed]: All ANSI characters should be removed from the XString, since they are not visible.
                string line = text[ma];
                // Calculate X and Y axis for the character
                int x = (windowDimensions[0] / 2 - line.Decolorize().Length / 2);
                int y = (windowDimensions[1] / 2 - textLines / 2 + ma);
                //
                Console.SetCursorPosition(x, y);
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Uses PrintCenteredText function to display the text,
        /// but changes the content during each tick to create cool hacker typing animation.
        /// </summary>
        /// <param name="text">
        /// Array of lines to display.
        /// </param>
        /// <param name="timePerTick">
        /// Custom delay time between ticks. The value is in MS.
        /// Recommended value: 10ms
        /// </param>
        /// <param name="ignoreLines">
        /// Set of numbers that represent indexes in the text array.
        /// These indexes (lines of text) will be printed before the animation begins.
        /// *
        /// Since the ignoreLines list will used on each tick, it's important to think about the performance.
        /// So I chose to use SortedSet instead of HashMap, since it doesn't have the o(1) problem.
        /// </param>
        /// <returns>
        /// The method is async, so it doesn't block the main thread during execution.
        /// The Task returns void.
        /// </returns>
        public static Task PrintCenteredTextWithAnimation(string[] text, int timePerTick = 5, SortedSet<int> ignoreLines = null)
        {
            // Put the animation in a new task.
            return Task.Run(() =>
            {
                // Used to access char[] of state
                StringBuilder[] currentState = new StringBuilder[text.Length];
                
                // A duplicate of the currentState array. Used as cache, since it's expensive to allocate a new array on each tick.
                string[] lastOutput = new string[text.Length];
            
                // Initialize the stringBuilders array 
                for (var ma = 0; ma < currentState.Length; ++ma)
                {
                    // If line should be ignored, push the state directly, otherwise set the default string.
                    string lineInitContent = (ignoreLines == null || !ignoreLines.Contains(ma))
                        ? new string(' ', text[ma].Length)
                        : text[ma];
                    
                    currentState[ma] = new StringBuilder(lineInitContent);
                }
                
                // Current cursor position in the input (text) array
                int[] currentOffset = { 0, 0 }; // x,y
                
                // Animation update loop The break statement will be called when the animation is finished.
                while (true)
                { // tick init
                    
                    // Calculate the new offset values
                    // We need to do it FIRST, because it also validates
                    // if the first line in the text array is not ignored.
                    
                    // REFACTOR: Second part of the IF statement: Excplicit instruction
                    if (currentOffset[0] > text[currentOffset[1]].Length - 1 || (currentOffset[1] == 0 && ignoreLines?.Contains(0) == true))
                    {
                        // Increment the offset Y till we don't find a string that should not be ignored.
                        while (true)
                        {
                            // Increment
                            currentOffset[1]++;
                            
                            // Check if ignored
                            if (ignoreLines == null || !ignoreLines.Contains(currentOffset[1])) break;
                        }
                        
                        // Reset the offset X
                        currentOffset[0] = 0;
                        
                        // Check if that's the end of the input array
                        // Break the loop if yes
                        if (currentOffset[1] > text.Length - 1)
                        {
                            break;
                        }
                    }
                    
                    // Update the currentOutput state
                    currentState[currentOffset[1]][currentOffset[0]] = text[currentOffset[1]][currentOffset[0]];

                    // Move to the next character in the line
                    currentOffset[0]++;

                    // Update lastOutput array. It will be used during the output process.
                    for (var ma = 0; ma < currentState.Length; ++ma)
                    {
                        lastOutput[ma] = currentState[ma].ToString();
                    }
                    
                    // Draw the current state using PrintCenteredText method
                    PrintCenteredText(lastOutput);
                    
                    // Wait some time before the next tick
                    Task.Delay(timePerTick).Wait();
                }
            });
        }

        /// <summary>
        /// Erases the console window.
        /// This is an external extension method, since I'm planing to make a static render pipeline.
        /// </summary>
        public static void ClearConsole()
        {
            // Erase current output
            Console.Clear();
        }
    }
}