using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Entity;

namespace FYPManager.Boundary.Services.ConsoleDisplay;

/// <summary>
/// This service is used to display a list of data in a paginated format.
/// </summary>
public class PaginatorService
{
    /// <summary>
    /// Displays the list of data in a paginated format.
    /// </summary>
    /// <typeparam name="T">The type of the list elements.</typeparam>
    /// <param name="list">The list of data to be displayed.</param>
    /// <param name="numPerPage">The number of elements to be displayed per page.</param>
    /// <param name="displayTitle">The title to be displayed at the top of the page.</param>
    /// <remarks>The list must contain objects that implement the <see cref="IDisplayable"/> interface.</remarks>
    public static void Paginate<T>(List<T> list, int numPerPage, string displayTitle) where T : IDisplayable
    {
        Console.Clear();

        if (list.Count == 0)
        {
            Console.WriteLine("No data to display. Enter any key to continue.");
            Console.ReadKey();
            return;
        }

        var numPages = (int)Math.Ceiling((double)list.Count / numPerPage);
        var currentPage = 1;

        while (true)
        {
            DisplayTopTitleText(displayTitle, currentPage, numPages);
            
            var start = (currentPage - 1) * numPerPage;
            var end = Math.Min(start + numPerPage, list.Count);

            for (var i = start; i < end; i++)
                DisplayData(list[i].ToString());

            DisplayBottomPageText();
            
            var page = NumberHandler.ReadInt(numPages);

            if (page == 0)
                return;

            currentPage = page;
        }
    }

    /// <summary>
    /// Displays the top title text of the current page.
    /// </summary>
    /// <param name="displayTitle">The title to be displayed.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="numPages">The total number of pages.</param>
    private static void DisplayTopTitleText(string displayTitle, int currentPage, int numPages)
    {
        DisplayBorder();
        Console.WriteLine($"{displayTitle}");
        Console.WriteLine($"Page {currentPage} of {numPages}");
        DisplayBorder();
    }

    /// <summary>
    /// Displays a single element of the list.
    /// </summary>
    /// <param name="data">The data to be displayed.</param>
    private static void DisplayData(string data)
    {
        DisplayBorder();
        Console.WriteLine(data);
        DisplayBorder();
    }

    /// <summary>
    /// Displays the bottom page text of the current page.
    /// </summary>
    private static void DisplayBottomPageText() => Console.WriteLine(
        $"╔══════════════════════════════════════════╗\n" +
        $"║ Enter page number to go to, or 0 to exit.║\n" +
        $"╚══════════════════════════════════════════╝");

    /// <summary>
    /// Displays the line border.
    /// </summary>
    private static void DisplayBorder() => Console.WriteLine(new string('═', 50));
}