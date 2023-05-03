using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;

namespace FYPManager.Boundary.Services.StrategySelector;

/// <summary>
/// Base class for view services that provide options to select filter and ordering strategy, and to display objects.
/// </summary>
/// <typeparam name="T">The type of the objects to be displayed.</typeparam>
public abstract class BaseViewService<T>
{
    /// <summary>
    /// Displays the options for selecting filter and ordering strategy, and to display objects.
    /// </summary>
    private static void DisplayOptions()
    {
        Console.Clear();
        Console.WriteLine(
            $"┌─────────────────────────────────┐\n" +
            $"│            OPTIONS              │\n" +
            $"├─────────────────────────────────┤\n" +
            $"│ (S) Select filter and ordering  │\n" +
            $"│ (D) Display                     │\n" +
            $"│ (B) Back                        │\n" +
            $"└─────────────────────────────────┘\n" +
            $"Please select an option:");
    }

    /// <summary>
    /// Gets the filter and ordering strategy to be used.
    /// </summary>
    /// <returns>The filter and ordering strategy.</returns>
    protected abstract FilterOrderStrategy<T> GetFilterOrderStrategy();

    /// <summary>
    /// Displays the list of objects using the specified filter and ordering strategy.
    /// </summary>
    /// <param name="elements">The list of objects to be displayed.</param>
    protected abstract void DisplayElements(List<T> elements);

    /// <summary>
    /// Runs the display service with the specified controller that implements the <see cref="IStrategyCompatible{T}"/> interface.
    /// </summary>
    /// <param name="controller">The controller that implements the <see cref="IStrategyCompatible{T}"/> interface.</param>
    public void RunDisplayService(IStrategyCompatible<T> controller)
    {
        var strategy = new FilterOrderStrategy<T>();

        while (true)
        {
            DisplayOptions();
            var option = StringHandler.ReadString("S", "D", "B");

            switch (option)
            {
                case "S":
                    strategy = GetFilterOrderStrategy();
                    break;

                case "D":
                    var elements = controller.GetListUsingStrategy(strategy);
                    DisplayElements(elements);
                    break;

                case "B":
                    return;
            }
        }
    }
}