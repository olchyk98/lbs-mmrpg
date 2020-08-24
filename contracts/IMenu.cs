namespace lbs_mmrpg.contracts
{
    public interface IMenu
    {
        /// <summary>
        /// Invokes the action at position [index] in the items dictionary.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// Returns false ONLY when the index is out of range.
        /// Returns true when the action was found and successfully executed(!).
        /// </returns>
        bool ExecuteItem(int index);

        void ChangeIndex(int direction);

        /// <summary>
        /// The function pauses the container thread and waits for
        /// the input.
        /// Once the ENTER button is pressed (and the user input is valid) the paused thread wlb released.
        /// </summary>
        /// <warnings>
        ///  * Method is recursive
        /// </warnings>
        void Display();

        void ExecuteSelectedItem();
    }
}