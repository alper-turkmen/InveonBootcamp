using System;
using System.Threading.Tasks;
using Gorev2.TaskMethods;

namespace Gorev2.MethodRunner
{
    public class TaskExamplesRunner
    {
        private readonly TaskExamples _taskExamples;

        public TaskExamplesRunner()
        {
            _taskExamples = new TaskExamples();
        }

        public async Task RunAllExamples()
        {
            Console.WriteLine("Task metot ornekleri calistiriliyor..\n");

            await _taskExamples.DelayExample();
            await _taskExamples.RunExample();

            var fromResult = await _taskExamples.FromResultExample();
            Console.WriteLine($"Task.FromResult sonucu: {fromResult}");

            await _taskExamples.WhenAllExample();
            await _taskExamples.WhenAnyExample();

            await _taskExamples.CompletedTaskExample();

            try
            {
                await _taskExamples.FromExceptionExample();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Task.FromException yakalandi: {ex.Message}");
            }

            var cancellationTokenSource = new System.Threading.CancellationTokenSource();
            try
            {
                cancellationTokenSource.Cancel();
                await _taskExamples.FromCanceledExample(cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task.FromCanceled islemi iptal edildi");
            }
        }
    }
}