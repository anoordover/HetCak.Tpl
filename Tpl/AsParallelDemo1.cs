using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tpl
{
    public static class AsParallelDemo1
    {
        
        public static void Run()
        {
            var lijst = CreateList();

            lijst.AsParallel()
                .WithDegreeOfParallelism(4)
                .ForAll(async i =>
                {
                    var result = await LongTask(i);
                    Console.WriteLine($"AsParallel2: {result}");
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

        private static IEnumerable<int> CreateList()
        {
            var lijst = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                lijst.Add(i);
            }

            return lijst;
        }
        
    }
}