await Example.Main();

class Example
{
    public static async Task Main()
    {
        //Console.WriteLine("Waiting for user input...");

        // Создаем несколько async операций, которые ждут ввода
        var tasks = new List<Task>();

        for (int i = 0; i < 5; i++)
        {
            int taskId = i;
            tasks.Add(Task.Run(async () => {
                //Console.WriteLine($"Task {taskId}: Started");

                // Имитируем ожидание I/O (не блокируем поток)
                await Task.Delay(TimeSpan.FromMinutes(10));

                //Console.WriteLine($"Task {taskId}: Completed");
            }));
        }

        // Корректное ожидание всех задач
        await Task.WhenAll(tasks);

        //Console.WriteLine("All tasks completed");
        Console.ReadLine();
    }
}