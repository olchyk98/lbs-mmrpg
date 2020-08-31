using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lbs_rpg.classes.gui.components
{
    public static class ConstantProgress
    {
        private const int MaxProgressWidth = 10;
        
        private static string PrintProgress(int progressValue)
        {
            // Create progress string
            StringBuilder progressBuilder = new StringBuilder();
            for (var ma = 0; ma < MaxProgressWidth; ++ma)
            {
                char current = (ma < progressValue) ? '░' : '▓';
                progressBuilder.Append(current);
            }

            // Extract string from the StringBuilder
            string progress = progressBuilder.ToString();

            // Return the progress bar
            return progress;
        }

        /// <summary>
        /// Displays a progress bar that can be interrupted by pressing any key.
        /// Executed in async mode (in the sync container).
        /// </summary>
        /// <param name="title">
        /// Label of the progress bar
        /// </param>
        /// <param name="tickDelay">
        /// Delay between ticks (ms)
        /// </param>
        /// <param name="onProgress">
        /// Action that will be invoked after every tick
        /// </param>
        public static void Start(string title, int tickDelay, Action onProgress)
        {
            // Initialize cancellation token
            var tokenSource = new CancellationTokenSource();
            var cancellationToken = tokenSource.Token;

            Task.Run(() =>
            {
                // Declare the output variable (cache)
                string[] content =
                {
                    title, "", default, default, "", "Press any key to stop"
                };

                // Current progress (chars)
                int progressValue = 0; // 0/progressWidth

                // Start drawing
                while (true)
                {
                    /*
                     * cancellationToken.ThrowIfCancellationRequested() can be used here,
                     * but to remove IDE warning I should use the break/return statement in the infinite while loop.
                     */
                    
                    // Check if the task was cancelled
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    // Clear the console
                    FastGuiUtils.ClearConsole();
                    
                    // Get content for the progess bar
                    string barContent = PrintProgress(progressValue);
                    content[2] = content[3] = barContent;
                    
                    // Output
                    FastGuiUtils.PrintCenteredText(content);

                    // Update progress value
                    if (++progressValue > MaxProgressWidth) progressValue = 0;

                    // Invoke callback 
                    onProgress();

                    // Tick delay
                    Task.Delay(tickDelay, cancellationToken).Wait(cancellationToken);
                }
            }, cancellationToken);

            // Listen for any input to kill the task
            Console.ReadKey(true);

            // Cancel the task after the input
            tokenSource.Cancel();
        }

        /// <summary>
        /// Displays a progress bar that will be paused when currentTicks will equal maxTicks.
        /// Executed in sync mode.
        /// </summary>
        /// <param name="title">
        /// Label of the progress bar
        /// </param>
        /// <param name="tickDelay">
        /// Delay in ms between ticks
        /// </param>
        /// <param name="maxTicks">
        /// Max ticks of the animation
        /// </param>
        /// <param name="onProgress">
        /// Action that invoked after each tick and returns status of the animation.
        /// If boolean equals true then onProgress invoked on the last animation tick.
        /// </param>
        /// <param name="onProgressTitleUpdate">
        /// If passed, will be invoked on each tick. The return value (string) will be used as the substatus content.
        /// </param>
        public static void Start(string title, int tickDelay, int maxTicks, Action<bool> onProgress, Func<float, string> onProgressTitleUpdate = null)
        {
            // Define Current progress (length of the bar)
            int progressValue = 0; // 0/progressWidth
            
            // Define currentTick
            int currentTick = 0;
            
            // Declare the output variable (cache)
            string[] content =
            {
                title, "", default, default, "",
                // Progress in procents
                "12%"
            };

            // Start drawing
            while (true)
            {
                // Clear the console
                FastGuiUtils.ClearConsole();
                    
                // Get content for the progess bar
                string barContent = PrintProgress(progressValue);
                content[2] = content[3] = barContent;
                
                // Update progress in procents
                float progressProcents = (float) currentTick / maxTicks;
                content[5] = onProgressTitleUpdate?.Invoke(progressProcents) ?? (progressProcents * 100).ToString("0") + "%";

                // Output
                FastGuiUtils.PrintCenteredText(content);

                // Update progress value
                if (++progressValue > MaxProgressWidth) progressValue = 0;
                
                // Update current tick value
                ++currentTick;
                
                // Check if it was the last tick
                bool isLastTick = currentTick >= maxTicks;

                // Invoke callback
                onProgress(isLastTick);
                
                // Stop if the last tick
                if (isLastTick) break;

                // Tick delay
                Task.Delay(tickDelay).Wait();
            }
        }
    }
}