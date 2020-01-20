using System;
using System.Threading;

namespace DeadlockRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            object locker1 = new object();
            object locker2 = new object();

            Thread thread1 = new Thread(() => {
                Console.WriteLine("Started Thread 1");
                lock (locker1)
                {
                    Console.WriteLine("Thread 1 locked Object 1");

                    Thread.Sleep(1000);

                    Console.WriteLine("Thread 1 attempting to lock Object 2");

                    lock (locker2) // Deadlock
                    {
                        Console.WriteLine("Thread 1 locked Object 2");
                    }
                }
                Console.WriteLine("Completed Thread 1");
            });

            Thread thread2 = new Thread(() =>
            {
                Console.WriteLine("Started Thread 2");
                lock (locker2)
                {
                    Console.WriteLine("Thread 2 locked Object 2");

                    Thread.Sleep(1000);

                    Console.WriteLine("Thread 2 attempting to lock Object 1");

                    lock (locker1) // Deadlock
                    {
                        Console.WriteLine("Thread 2 locked Object 1");
                    }
                }
                Console.WriteLine("Completed Thread 2");
            });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Main program completed");
        }
    }
}
