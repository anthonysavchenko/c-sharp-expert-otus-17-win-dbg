
using ConsoleApp1;

Console.WriteLine("Запуск программы...");

Thread thread1 = new Thread(Process.Method1);
Thread thread2 = new Thread(Process.Method2);

thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

Console.WriteLine("Программа завершена");
