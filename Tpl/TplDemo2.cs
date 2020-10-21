using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Tpl
{
    public static class TplDemo2
    {
        public static async Task Run()
        {
            var tr2Block = new TransformBlock<int, int>(
                x =>
                {
                    var result = new Task<int>(() =>
                    {
                        Thread.Sleep(1000);
                        return x + x + x;
                    });
                    result.Start();
                    return result;
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 2
                });
            var tr3Block = new TransformBlock<int, string>(
                n => n.ToString(CultureInfo.InvariantCulture));
            var linkOptions = new DataflowLinkOptions {PropagateCompletion = true};
            tr2Block.LinkTo(tr3Block, linkOptions);

            for (var i = 0; i < 10; i++)
            {
                tr2Block.Post(i);
            }

            tr2Block.Complete();
            while (await tr3Block.OutputAvailableAsync())
            {
                while (tr3Block.TryReceive(out var item))
                {
                    Console.WriteLine($"TPL2: {item}");
                }
            }

            await tr3Block.Completion;
        }

    }
}