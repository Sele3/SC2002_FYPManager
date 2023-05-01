namespace FYPManager.Boundary.Services.InputHandlers;

/// <summary>
/// Utility class to handle user input of strings and characters.
/// </summary>
internal static class StringHandler
{
    /// <summary>
    /// Reads a non-empty string input. 
    /// </summary>
    /// <returns>The string input by the user.</returns>
    public static string ReadString()
    {
        string text;
        while (string.IsNullOrEmpty(text = Console.ReadLine()!.Trim()))
            Console.WriteLine("Error. Input cannot be empty");
        return text;
    }

    /// <summary>
    /// Reads a string input from a list of valid strings.
    /// </summary>
    /// <param name="list">List of valid strings.</param>
    /// <returns>The string input by the user.</returns>
    public static string ReadString(params string[] list)
    {
        string text;
        while (!list.Contains(text = ReadString()))
            Console.WriteLine("Error. Invalid input.");
        return text;
    }
    /// <summary>
    /// Reads a "Y" or "N" input.
    /// </summary>
    /// <returns><see langword="true"/> if the user inputs a "Y"; <see langword="false"/> otherwise.</returns>
    public static bool GetYesNo() => ReadString("Y", "N").Equals("Y");

    /// <summary>
    /// Reads a non-empty character input.
    /// </summary>
    /// <returns>The character input by the user.</returns>
    public static char ReadCharacter()
    {
        string text;
        while ((text = ReadString()).Length > 1)
            Console.WriteLine("Error. Input should only contain a single character.");
        return text.ElementAt(0);
    }
}