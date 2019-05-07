using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// https://exprotocol.com/2017/02/15/set-timeout-for-a-long-running-process-using-c/
/// </summary>
namespace TaskTimeoutForLongRunningProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            var activity = new Activity();
            activity.Setup().Wait();

            Console.ReadLine();
        }

        public class Activity
        {
            private CancellationTokenSource tokenSource;

            public async Task Setup()
            {
                TimeSpan timeoutValue = new TimeSpan(0, 0, 2);

                tokenSource = new CancellationTokenSource();

                var task = Task.Run(() => Run(), tokenSource.Token);

                //Check the task is delaying
                if (await Task.WhenAny(task, Task.Delay(timeoutValue)) == task)
                {
                    // task completed within the timeout
                    Console.WriteLine("Task Completed Successfully");
                }
                else
                {
                    // timeout => cancel the task
                    tokenSource.Cancel();

                    Console.WriteLine("Time Out. Aborting Task");

                    try
                    {
                        task.GetAwaiter().GetResult(); //Waiting for the task to throw OperationCanceledException
                    }
                    catch (TaskCanceledException)
                    {

                    }
                }
            }

            /// <summary>
            /// Long running process
            /// </summary>
            public void Run()
            {
                int action = 1;

                try
                {
                    while (true)
                    {
                        if (tokenSource.Token.IsCancellationRequested)
                            tokenSource.Token.ThrowIfCancellationRequested();  //Stop if the cancellation requested

                        Console.WriteLine("Running action " + action++);

                        Thread.Sleep(200);
                    }

                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Task Aborted");
                }
            }
        }
    }
}
