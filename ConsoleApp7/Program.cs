Example.Main();

class Example
{
    static Dictionary<string, byte[]> cache = new Dictionary<string, byte[]>();
    const int MAX_CACHE_SIZE = 1000;
    static Queue<string> accessOrder = new Queue<string>();

    public static void Main()
    {
        // Заполняем кэш с автоматическим вытеснением старых элементов
        for (int i = 0; i < 5000; i++)
        {
            string key = $"key_{i}";

            if (cache.Count >= MAX_CACHE_SIZE)
            {
                // Удаляем самый старый элемент
                string oldKey = accessOrder.Dequeue();
                cache.Remove(oldKey);
            }

            cache[key] = new byte[50 * 1024]; // 50KB
            accessOrder.Enqueue(key);

            Thread.Sleep(5);
        }

        //Console.WriteLine($"Cache size: {cache.Count}");
        Console.ReadLine();
    }
}
