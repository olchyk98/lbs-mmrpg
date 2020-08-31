using System;

namespace lbs_rpg.classes.gui.components
{
    // REFACTORED [4.5b]: ENUM -> CLASS
    internal static class MinimalScreenResolution
    {
        public static readonly int ScreenWidth = 100;
        public static readonly int ScreenHeight = 25;
    }

    public static class ResolutionHandler
    {
        /// <summary>
        /// Returns width and height of the console container.
        /// </summary>
        /// <returns></returns>
        public static int[] GetResolution()
        {
            return new[]
            {
                Console.WindowWidth,
                Console.WindowHeight
            };
        }

        /// <summary>
        /// Returns width or height of the console container
        /// </summary>
        /// <returns></returns>
        public static int GetResolution(int index)
        {
            // Get resolution %> resolution
            // REFACTOR: Not readable
            int resolution = (index == 0 || index == 1)
                ? (
                    (index == 1) ? Console.WindowHeight : Console.WindowWidth
                )
                : default;

            // Validate resolution
            if (resolution == default)
            {
                throw new ArgumentException("Invalid argument. The value can be only 0 or 1");
            }

            return resolution;
        }

        /// <summary>
        /// Checks if the console container is not too small.
        /// </summary>
        /// <returns></returns>
        public static bool IsSupportedResolution()
        {
            int[] resolution = GetResolution();
            return resolution[0] >= MinimalScreenResolution.ScreenWidth &&
                   resolution[1] >= MinimalScreenResolution.ScreenHeight;
        }
    }
}