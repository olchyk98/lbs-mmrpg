using System;
using System.Collections.Generic;
using System.Linq;
using lbs_rpg.classes.gui.components.colorize;
using lbs_rpg.contracts;

namespace lbs_rpg.classes.gui.components
{
    public class Menu : IMenu
    {
        private int _selectedIndex = 0;
        private readonly Dictionary<string, Action> _items = null;
        private string _label = null;

        /*
            [string, function]
        */
        public Menu(Dictionary<string, Action> items, string label = null)
        {
            _items = items;
            _label = label;
        }

        /// <summary>
        /// Invokes the action at position [index] in the items dictionary.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// Returns false ONLY when the index is out of range.
        /// Returns true when the action was found and successfully executed(!).
        /// </returns>
        public bool ExecuteItem(int index)
        {
            string itemLabel = _items.Keys.ElementAtOrDefault(index);

            // Skip if 
            if (itemLabel == null) return false;

            // Get and invoke the action
            _items[itemLabel].Invoke();

            // Return the success
            return true;
        }

        /// <summary>
        /// Locks the thread and waits for the user [char] input
        /// </summary>
        /// <returns>
        /// Returns 0 if user pressed ENTER
        /// Returns 1 if user pressed BOTTOM ARROW
        /// Returns -1 if user pressed UPPER ARROW
        /// Otherwise, returns null
        /// </returns>
        /// <warning>
        /// The return value is linked wth the Display method. That is not good,
        /// I did that anyway for the aesthetics sake.
        ///
        /// The Display method checks if the return value equals 0 and uses
        /// that value to control the input if it's not.
        /// </warning>
        private static int? GetKeyboardInput()
        {
            // Get the input (ReadKey)
            ConsoleKeyInfo pressedKeyInfo = Console.ReadKey(true);

            // Get information about the pressed key
            ConsoleKey pressedKey = pressedKeyInfo.Key;

            // Process input
            int? returnValue = null;

            switch (pressedKey)
            {
                case ConsoleKey.Enter:
                    returnValue = 0;
                    break;
                case ConsoleKey.DownArrow:
                    returnValue = 1;
                    break;
                case ConsoleKey.UpArrow:
                    returnValue = -1;
                    break;
            }

            // Return the action
            return returnValue;
        }

        public void ChangeIndex(int direction)
        {
            // 1 - down, -1 - up

            // Validate if dirrection is valid
            if (direction != 1 && direction != -1)
            {
                throw new Exception(
                    $"Fired [changeIndex] method with an invalid argument value -> direction:{direction}");
            }

            // Validate if next position is not of the boundaries
            int nextIndex = _selectedIndex + direction;

            // Just skip if it's true
            if (nextIndex < 0 || nextIndex > _items.Count - 1) return;

            // Update the position cursor
            _selectedIndex = nextIndex;
        }

        /// <summary>
        /// The function pauses the container thread and waits for
        /// the input.
        /// Once the ENTER button is pressed (and the user input is valid) the paused thread wlb released.
        /// </summary>
        /// <warnings>
        ///  * Method is recursive
        /// </warnings>
        public void Display(bool clearScreen = true)
        {
            if (clearScreen)
            {
                // Clear the console
                FastGuiUtils.ClearConsole();
            }

            // Declare print output array
            string[] items = _items.Keys.ToArray();

            // Stylize items
            for (var ma = 0; ma < items.Length; ma++)
            {
                // Reference the array item
                ref string item = ref items[ma];

                // Highlight if currently selected
                if (ma == _selectedIndex)
                {
                    item = item.Colorize("bgyellow").Colorize("bgwhite");
                    continue; // if yes, go to the next item
                }

                // Otherwise set the default color
                item = item.Colorize("red");
            }

            // Push label to the output without any stylization
            items = items.Prepend(_label).ToArray();

            // Display the options
            FastGuiUtils.PrintCenteredText(items);

            // Using while loop to prevent redundant menu redrawing
            // [RIDER]: Message "Expression is always true" should be ignored,
            // since the program works fine and this message is caused by some kind of IDE bug.
            while (true)
            {
                // Wait for user input (code->event)
                int? pressCodeNullable = GetKeyboardInput();

                // Continue input listening if no event returned
                if (pressCodeNullable == null) continue;

                // Convert pressCode to int if an event was returned
                int pressCode = (int) pressCodeNullable;

                // Process SUBMIT_ACTION
                if (pressCode == 0)
                {
                    ExecuteSelectedItem();
                    return;
                }

                // Process MOVE_ACTION
                // FIXME<[delayed]: Really bad implementation
                ChangeIndex(pressCode);

                // Break the while loop, because we've got an valid input
                // and the menu state has been updated
                break;
            }

            // Since the method didn't return anything yet, we refresh the menu by displaying it again
            // DEVNOTE[olesodynets]: "can be replaced with a loop, but i dont want to nest the whole function"
            Display(clearScreen);
        }

        public void ExecuteSelectedItem()
        {
            ExecuteItem(_selectedIndex);
        }
    }
}