Example.Main();

class Example
{
    static object sharedLock = new object();

    ~Example()
    {
        lock (sharedLock) // Финализатор захватывает блокировку
        {
            Thread.Sleep(10000); // И долго держит
        }
    }

    public static void Main()
    {
        lock (sharedLock) // Main тоже захватывает блокировку
        {
            for (int i = 0; i < 1000; i++)
            {
                new Example();
            }
            GC.Collect();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}

