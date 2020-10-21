using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Tpl
{
    public static class TplDemo1
    {
        public static Task Run()
        {
            var trBlock = new TransformBlock<int, int>(
                x =>
                {
                    var result = new Task<int>(() =>
                    {
                        Thread.Sleep(1000);
                        return x + x;
                    });
                    result.Start();
                    return result;
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 2
                });
            var actionBlock = new ActionBlock<int>(n =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"TPL2: {n}");
                });
            var linkOptions1 = new DataflowLinkOptions {PropagateCompletion = true};
            trBlock.LinkTo(actionBlock, linkOptions1);
            for (var i = 0; i < 10; i++)
            {
                trBlock.Post(i);
            }

            trBlock.Complete();
            Console.WriteLine("Done");
            return actionBlock.Completion;
        }

    }
}