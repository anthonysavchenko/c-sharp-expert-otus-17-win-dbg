Example.Main();

class Example
{
    public event EventHandler MyEvent;
    static List<Example> publishers = new List<Example>();

    public static void Main()
    {
        var longLived = new LongLivedObject();

        for (int i = 0; i < 10000; i++)
        {
            var publisher = new Example();
            publisher.MyEvent += longLived.Handler; // Забыли отписаться
            publishers.Add(publisher);
        }

        GC.Collect();
        Console.ReadLine();
    }
}

class LongLivedObject
{
    private byte[] data = new byte[10 * 1024]; // 10KB
    public void Handler(object sender, EventArgs e) { }
}
