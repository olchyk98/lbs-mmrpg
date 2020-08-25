using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lbs_rpg.classes.gui.components
{
    public static class ConstantProgress
    {
        public static void Start(int tickDelay, Action onProgress)
        {
            var tokenSource = new CancellationTokenSource();
            var cancellationToken = tokenSource.Token;
            
            Task.Run(() =>
            {
                // Declare the output variable (cache)
                string[] content = {"SLEEPING", "", default, default};

                // Declare progress line length
                int progressWidth = 10;

                // Progress value
                int progressValue = 0; // 0/progressWidth

                // Start drawing
                while (true)
                {
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

                    // Tick delay
                    Thread.Sleep(tickDelay);

                    // Invoke callback 
                    onProgress();
                }
            }, cancellationToken);

            Console.ReadKey(true);
            Console.WriteLine("a");
        }
    }
}