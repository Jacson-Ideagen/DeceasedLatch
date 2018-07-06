using System;
using System.Diagnostics;
using System.Threading;

namespace DeceasedLatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            prog.Run();
        }

        public void Run()
        {
            DataStorerDeadlock dataStorer = new DataStorerDeadlock();
            while (true)
            {
                dataStorer.ConcurrencyTest();
                Console.ReadLine();
            }
        }
    }
}
