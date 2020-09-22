namespace lbs_rpg.classes.gui.components.colorize
{
    public static class SupportOs
    {
#if win32 || win64
        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        
        public static void SupportWindows()
        {
            // Skip if not windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            
            var iStdOut = GetStdHandle(-11);
            if (!GetConsoleMode(iStdOut, out var outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }
            outConsoleMode |= 0x0004 | 0x0008;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.ReadKey();
                return;
            } 
        }
#else
        public static void SupportWindows()
        {
        }
#endif
    }
}