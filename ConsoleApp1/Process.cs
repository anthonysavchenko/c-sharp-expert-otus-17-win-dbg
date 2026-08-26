namespace ConsoleApp1;

public static class Process
{
    private static readonly object lock1 = new object();
    private static readonly object lock2 = new object();

    public static void Method1()
    {
        lock (lock1)
        {
            //Console.WriteLine("Thread 1: Захватил lock1");
            Thread.Sleep(100); // Даем время второму потоку захватить lock2

            //Console.WriteLine("Thread 1: Ожидает lock2");
            lock (lock2)
            {
                //Console.WriteLine("Thread 1: Захватил lock2");
            }
        }
    }

    public static void Method2()
    {
        lock (lock2)
        {
            //Console.WriteLine("Thread 2: Захватил lock2");
            Thread.Sleep(100); // Даем время первому потоку захватить lock1

            //Console.WriteLine("Thread 2: Ожидает lock1");
            lock (lock1)
            {
                //Console.WriteLine("Thread 2: Захватил lock1");
            }
        }
    }

}
