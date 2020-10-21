using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Tpl
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=========== Begin Demo ==========");
            await TplDemo2.Run();
            Console.WriteLine("=========== Einde Demo ==========");
            Thread.Sleep(10000);
            /*
            Console.WriteLine("=========== Demo As Parallel 2 ===========");
            AsParallelDemo2.Run();
            Console.WriteLine("=========== Einde Demo As Parallel 2 ===========");

            Console.WriteLine("========== Demo TPL 1 ============");
            await TplDemo1.Run();
            Console.WriteLine("========== Einde Demo TPL 1 ============");
            
            Console.WriteLine("========== Demo TPL 2 ============");
            await TplDemo2.Run();
            Console.WriteLine("========== Einde Demo TPL 2 ============");
            */
        }


    }
}