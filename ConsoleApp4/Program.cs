try
{
    RecursiveMethod(0);
}
catch (Exception ex)
{
    //Console.WriteLine(ex.Message);
    Console.ReadLine();
}

void RecursiveMethod(int depth)
{
    RecursiveMethod(depth + 1); // Нет условия выхода
}