using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DeceasedLatch
{
    public class DataStorerDeadlock
    {
        private class DataStore { public int Value { get; set; } }

        private DataStore store = new DataStore();
        private DataStore emporium = new DataStore();

        private Random r = new Random();
        private int chance = 50;
        public void ConcurrencyTest()
        {
            var thread1 = new Thread(IncrementTheStoreFirst);
            var thread2 = new Thread(IncrementTheEmporiumFirst);
            chance += 10;
            chance = chance > 110 ? 110 : chance;
            chance = chance < 10 ? 10 : chance;
            chance = 110 - chance + 10;
            thread1.Start();
            thread2.Start();

            thread1.Join(); // Wait for the thread to finish executing
            thread2.Join();

            Console.WriteLine($"Final value: {store.Value}");
        }

        private void IncrementTheStoreFirst()
        {
            Thread.Sleep(r.Next(10, chance));
            lock (store)
            {
                store.Value++;

                lock (emporium)
                {
                    emporium.Value++;
                }
            }
        }
        private void IncrementTheEmporiumFirst()
        {
            Thread.Sleep(r.Next(10, chance));
            lock (emporium)
            {
                emporium.Value++;
                lock (store)
                {
                    store.Value++;
                }
            }
        }
    }
}
