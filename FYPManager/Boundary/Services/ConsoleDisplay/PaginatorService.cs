using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Entity;

namespace FYPManager.Boundary.Services.ConsoleDisplay;

public class PaginatorService
{
    public static void Paginate<T>(List<T> list, int numPerPage, string displayTitle) where T : IDisplayable
    {
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

    private static void DisplayTopTitleText(string displayTitle, int currentPage, int numPages)
    {
        Console.Clear();

        DisplayBorder();
        Console.WriteLine($"{displayTitle}");
        Console.WriteLine($"Page {currentPage} of {numPages}");
        DisplayBorder();
    }

    private static void DisplayData(string data)
    {
        DisplayBorder();
        Console.WriteLine(data);
        DisplayBorder();
    }

    private static void DisplayBottomPageText() => Console.WriteLine(
        $"╔══════════════════════════════════════════╗\n" +
        $"║ Enter page number to go to, or 0 to exit.║\n" +
        $"╚══════════════════════════════════════════╝");

    private static void DisplayBorder() => Console.WriteLine(new string('═', 50));
}