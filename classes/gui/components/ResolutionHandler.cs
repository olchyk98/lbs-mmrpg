using System;

namespace lbs_mrpg.classes.gui.components
{
    internal enum MinimalScreenResolution
    {
        SCREEN_WIDTH = 100,
        SCREEN_HEIGHT = 25,
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
        /// Checks if the console container is not too small.
        /// </summary>
        /// <returns></returns>
        public static bool IsSupportedResolution()
        {
            int[] resolution = GetResolution();

            return resolution[0] >= (int) MinimalScreenResolution.SCREEN_WIDTH &&
                   resolution[1] >= (int) MinimalScreenResolution.SCREEN_HEIGHT;
        }
    }
}