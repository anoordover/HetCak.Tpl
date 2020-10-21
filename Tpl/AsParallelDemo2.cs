using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tpl
{
    public static class AsParallelDemo2
    {
        public static void Run()
        {
            var lijst = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                lijst.Add(i);
            }

            lijst.AsParallel()
                .WithDegreeOfParallelism(4)
                .ForAll(i =>
                {
                    var result = LongTask(i).Result;
                    Console.WriteLine($"AsParallel1: {result}");
                });
        }
        private static Task<int> LongTask(int x)
        {
            var longTask = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                return x + x;
            });
            longTask.Start();
            return longTask;
        }

    }
}