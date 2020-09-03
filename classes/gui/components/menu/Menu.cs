using System;
using System.Collections.Generic;
using System.Linq;
using lbs_rpg.classes.gui.components.colorize;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.classes.gui.components.menu
{
    public class Menu : IMenu
    {
        private int mySelectedIndex = 0;
        private readonly Dictionary<string, Action<int>> myItems = null;
        private readonly string myLabel = null;
        
        /// <summary>
        /// Menu Constructor
        /// </summary>
        /// <param name="items">
        /// Menu options.
        /// Key: Action{int:selectedIndex}
        /// </param>
        /// <param name="label">
        /// Menu title
        /// </param>
        /// <param name="selectedIndex">
        /// Very useful if you don't want cursor to be reseted after refresh, allows you
        /// natively set cursor at the previous position.
        /// </param>
        public Menu(Dictionary<string, Action<int>> items, string label = null, int selectedIndex = default)
        {
            myItems = ValidateListOptions(items);
            myLabel = label;

            // Process selected index
            // Ignore selectedIndex if out of the range
            if (selectedIndex != default && selectedIndex >= 0 && selectedIndex <= myItems.Count)
            {
                mySelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Validates menu options that are passed in the items param and casts an exception if something is wrong.
        /// </summary>
        /// <param name="items"></param>
        /// <returns>
        /// The original items array
        /// </returns>
        /// <exception cref="ApplicationException">
        /// Throwed if any of items in the items dictionary initialized with mistakes.
        /// </exception>
        private Dictionary<string, Action<int>> ValidateListOptions(Dictionary<string, Action<int>> items)
        {
            foreach (KeyValuePair<string, Action<int>> keyValue in items)
            {
                if (keyValue.Value == null)
                {
                    throw new ApplicationException(
                        $"Tried to pass an invalid menu option. The option {keyValue.Key} lacks action processor!");
                }
            }

            // Return items
            return items;
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
            string itemLabel = myItems.Keys.ElementAtOrDefault(index);

            // Skip if 
            if (itemLabel == null) return false;

            // Get and invoke the action
            myItems[itemLabel]?.Invoke(mySelectedIndex);

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
        private static MenuKeyboardEvent GetKeyboardInput()
        {
            // Get the input (ReadKey)
            ConsoleKeyInfo pressedKeyInfo = Console.ReadKey(true);

            // Get information about the pressed key
            ConsoleKey pressedKey = pressedKeyInfo.Key;

            // Process input
            MenuKeyboardEvent returnValue = MenuKeyboardEvent.InvalidKey;

            switch (pressedKey)
            {
                case ConsoleKey.Enter:
                    returnValue = MenuKeyboardEvent.Submit;
                    break;
                case ConsoleKey.DownArrow:
                    returnValue = MenuKeyboardEvent.MoveDown;
                    break;
                case ConsoleKey.UpArrow:
                    returnValue = MenuKeyboardEvent.MoveUp;
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
            int nextIndex = mySelectedIndex + direction;

            // Skip to the first/last item if index doesn't exist
            if (nextIndex < 0)
            {
                nextIndex = myItems.Count - 1;
            } else if (nextIndex > myItems.Count - 1)
            {
                nextIndex = 0;
            }

            // Update the position cursor
            mySelectedIndex = nextIndex;
        }

        /// <summary>
        /// The function pauses the container thread and waits for
        /// the input.
        /// Once the ENTER button is pressed (and the user input is valid) the paused thread wlb released.
        /// </summary>
        /// <warnings>
        ///  * Method is recursive
        /// </warnings>
        public void Display()
        {
            FastGuiUtils.ClearConsole();

            // Take all menu options -> output array
            string[] items = myItems.Keys.ToArray();
            
            // Stylize items
            for (var ma = 0; ma < items.Length; ma++)
            {
                // Reference the array item
                ref string item = ref items[ma];

                // Highlight if currently selected
                if (ma == mySelectedIndex)
                {
                    item = item.Colorize("white").Colorize("bgyellow");
                    continue; // if yes, go to the next item
                }

                // Otherwise set the default color
                item = item.Colorize("red");
            }

            // Push label to the output without any stylization
            items = items.Prepend(string.Empty).Prepend(myLabel).ToArray();

            // Display the options
            FastGuiUtils.PrintCenteredText(items);

            // Using while loop to prevent redundant menu redrawing
            // [RIDER]: Message "Expression is always true" should be ignored,
            // since the program works fine and this message is caused by some kind of IDE bug.
            while (true)
            {
                // Wait for user input (code->event)
                MenuKeyboardEvent pressedEvent = GetKeyboardInput();

                // Continue input listening if no valid action returned
                if (pressedEvent == MenuKeyboardEvent.InvalidKey) continue;

                // Process action
                switch (pressedEvent)
                {
                    case MenuKeyboardEvent.Submit:
                        ExecuteSelectedItem();
                        return;
                    case MenuKeyboardEvent.MoveUp:
                        ChangeIndex(-1);
                        break;
                    case MenuKeyboardEvent.MoveDown:
                        ChangeIndex(1);
                        break;
                    default: // Invalid event
                        throw new Exception($"Received an unexpected input event: { pressedEvent.ToString() }");
                }

                // Break the while loop, because we've got a valid input
                // and the menu state has been updated
                break;
            }

            // Since the method didn't return anything yet, we refresh the menu by displaying it again
            // DEVNOTE[olesodynets]: "can be replaced with a loop, but i dont want to nest the whole function"
            Display();
        }

        public void ExecuteSelectedItem()
        {
            ExecuteItem(mySelectedIndex);
        }
    }
}