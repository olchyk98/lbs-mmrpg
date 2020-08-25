using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lbs_rpg.classes.gui.components
{
    public static class ConstantProgress
    {
        public static void Start(string title, int tickDelay, Action onProgress)
        {
            var tokenSource = new CancellationTokenSource();
            var cancellationToken = tokenSource.Token;
            
            Task.Run(() =>
            {
                // Declare the output variable (cache)
                string[] content =
                {
                    title, "", default, default, "", "Press any key to stop"
                };

                // Declare progress line length
                int progressWidth = 10;

                // Progress value
                int progressValue = 0; // 0/progressWidth

                // Start drawing
                while (true)
                {
                    // Check if the task was cancelled
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    FastGuiUtils.ClearConsole();

                    // Create progress string
                    StringBuilder progressBuilder = new StringBuilder();
                    for (int ma = 0; ma < progressWidth; ++ma)
                    {
                        char current = (ma < progressValue) ? '░' : '▓';
                        progressBuilder.Append(current);
                    }

                    string progress = progressBuilder.ToString();

                    // Push the progress bar
                    content[2] = progress;
                    content[3] = progress;

                    // Output
                    FastGuiUtils.PrintCenteredText(content);

                    // Update progress value
                    if (++progressValue > progressWidth) progressValue = 0;
                    
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
            
            // ...
            Console.WriteLine("a");
        }
    }
}