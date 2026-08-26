// Забиваем весь ThreadPool долгими задачами
for (int i = 0; i < 1000; i++)
{
    ThreadPool.QueueUserWorkItem(_ => Thread.Sleep(60000));
}

//Console.WriteLine("ThreadPool забит!");
Console.ReadLine();