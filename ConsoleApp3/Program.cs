List<byte[]> leak = new List<byte[]>();

for (int i = 0; i < 10000; i++)
{
    leak.Add(new byte[100 * 1024]); // 100KB каждый
    Thread.Sleep(10);
}
Console.ReadLine();