namespace FYPManager.Boundary.Services.InputHandlers;

/// <summary>
/// Utility class to handle user input of enums.
/// </summary>
/// <typeparam name="T">The type of enum to handle.</typeparam>
public static class EnumHandler<T> where T : Enum
{
    private static readonly IReadOnlyList<T> values;

    static EnumHandler()
    {
        values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }

    /// <summary>
    /// Displays a list of available options for the enum.
    /// </summary>
    /// <param name="name">The name of the enum.</param>
    private static void DisplayEnumList(string name)
    {
        Console.WriteLine($"Available options for {name}:");
        for (int i = 0; i < values.Count; i++)
            Console.WriteLine($"{i + 1}. {values[i]}");

        Console.WriteLine("Please select an option: ");
    }

    /// <summary>
    /// Prompts the user to select an enum value.
    /// </summary>
    /// <param name="name">The name of the enum.</param>
    /// <returns>The selected enum value.</returns>
    public static T ReadEnum(string name)
    {
        DisplayEnumList(name);
        var idx = NumberHandler.ReadInt(1, values.Count);
        return values[idx - 1];
    }
}