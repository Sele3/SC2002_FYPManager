namespace FYPManager.Boundary.Services;

/// <summary>
/// Utility class to handle user input of integers and doubles.
/// </summary>
internal static class NumberHandler
{
    //// <summary>
    /// Reads and returns an integer from user input, prompting again if invalid input is given.
    /// </summary>
    /// <returns>The integer input by the user.</returns>
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

    /// <summary>
    /// Reads and returns an integer from user input within a specified range, prompting again if invalid input is given.
    /// </summary>
    /// <param name="lo">The lower bound of the allowed range.</param>
    /// <param name="hi">The upper bound of the allowed range.</param>
    /// <returns>The integer input by the user within the specified range.</returns>
    public static int ReadInt(int lo, int hi)
    {
        int num;
        while (!(lo <= (num = ReadInt()) && num <= hi))
            Console.WriteLine("Error. Integer entered must be between {0} and {1}", lo, hi);
        return num;
    }

    /// <summary>
    /// Reads and returns a non-negative integer from user input up to a specified maximum value, prompting again if invalid input is given.
    /// </summary>
    /// <param name="hi">The maximum allowed value.</param>
    /// <returns>The integer input by the user, between 0 and the specified maximum value.</returns>
    public static int ReadInt(int hi) => ReadInt(0, hi);

    /// <summary>
    /// Reads and returns an integer from user input greater than or equal to a specified minimum value, prompting again if invalid input is given.
    /// </summary>
    /// <param name="lo">The minimum allowed value.</param>
    /// <returns>The integer input by the user, greater than or equal to the specified minimum value.</returns>
    public static int ReadIntFrom(int lo)
    {
        int num;
        while ((num = ReadInt()) < lo)
            Console.WriteLine("Error. Integer entered must be >= {0}", lo);
        return num;
    }

    /// <summary>
    /// Reads and returns a double from user input, prompting again if invalid input is given.
    /// </summary>
    /// <returns>The double input by the user.</returns>
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