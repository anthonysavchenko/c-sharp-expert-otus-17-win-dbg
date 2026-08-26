object lock1 = new object();
object lock2 = new object();
int counter = 0;

// Выглядит как дедлок, но порядок блокировок одинаковый!
var threads = new List<Thread>();

for (int i = 0; i < 50; i++)
{
    threads.Add(new Thread(() => {
        lock (lock1)        // Всегда сначала lock1
        {
            Thread.Sleep(10);
            lock (lock2)    // Потом lock2
            {
                counter++;
            }
        }
    }));
}

threads.ForEach(t => t.Start());
threads.ForEach(t => t.Join());

//Console.WriteLine($"Counter: {counter}");
Console.ReadLine();
