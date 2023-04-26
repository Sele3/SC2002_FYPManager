namespace FYPManager.Boundary.Services;

internal static class NumberHandler
{
    public static int ReadInt()
    {
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out int num))
            {
                Console.WriteLine("Error. Input must be an integer.");
                continue;
            }

            if (num >= 0)
                return num;

            Console.WriteLine("Error. Integer entered must be >= 0.");
        }
    }

    public static int ReadInt(int lo, int hi)
    {
        int num;
        while (!(lo <= (num = ReadInt()) && num <= hi))
            Console.WriteLine("Error. Integer entered must be between {0} and {1}", lo, hi);
        return num;
    }

    public static int ReadInt(int hi) => ReadInt(0, hi);

    public static int ReadIntFrom(int lo)
    {
        int num;
        while ((num = ReadInt()) < lo)
            Console.WriteLine("Error. Integer entered must be >= {0}", lo);
        return num;
    }

    public static double ReadDouble()
    {
        while (true)
        {
            if (!double.TryParse(Console.ReadLine(), out double num))
            {
                Console.WriteLine("Error. Input must be a double.");
                continue;
            }

            if (num >= 0)
                return num;
            Console.WriteLine("Error. Double entered must be >= 0.");
        }
    }
}