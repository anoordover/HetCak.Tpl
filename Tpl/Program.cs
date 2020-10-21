using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Tpl
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var trBlock = new TransformBlock<int, int>(
                x => x + x);
            var actionBlock = new ActionBlock<int>(n =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(n);
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 2
                });
            var linkOptions = new DataflowLinkOptions {PropagateCompletion = true};
            trBlock.LinkTo(actionBlock, linkOptions);
            for (var i = 0; i < 10; i++)
            {
                trBlock.Post(i);
            }

            trBlock.Complete();
            Console.WriteLine("Done");
            await actionBlock.Completion;

            Console.WriteLine("======================");
            trBlock = new TransformBlock<int, int>(
                x => Task.FromResult(x + x + x));
            var tr2Block = new TransformBlock<int, string>(
                n =>
                {
                    Thread.Sleep(1000);
                    return n.ToString(CultureInfo.InvariantCulture);
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 4
                });
            trBlock.LinkTo(tr2Block, linkOptions);

            for (var i = 0; i < 10; i++)
            {
                trBlock.Post(i);
            }

            trBlock.Complete();
            while (await tr2Block.OutputAvailableAsync())
            {
                while (tr2Block.TryReceive(out var item))
                {
                    Console.WriteLine(item);
                }
            }

            await tr2Block.Completion;
        }
    }
}