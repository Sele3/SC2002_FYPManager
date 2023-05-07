using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;

namespace FYPManager.Boundary.Services.StrategySelector;

/// <summary>
/// This abstract class provides the functionality to select a filter and order strategy for viewing entities.
/// </summary>
/// <typeparam name="T">The type of entities to select a strategy for.</typeparam>
public abstract class BaseStrategySelector<T>
{
    /// <summary>
    /// The filter and order strategy to use.
    /// </summary>
    protected FilterOrderStrategy<T> Strategy { get; set; }
    /// <summary>
    /// A dictionary of menu options and corresponding actions to perform when selected.
    /// </summary>
    private Dictionary<int, Action> ChoiceDict { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseStrategySelector{T}"/> class.
    /// </summary>
    public BaseStrategySelector()
    {
        Strategy = new();
        ChoiceDict = new();
    }

    /// <summary>
    /// Displays the menu for selecting a filter and order strategy.
    /// </summary>
    protected abstract void DisplayMenu();

    /// <summary>
    /// Selects a custom filter and order strategy chosen by the user.
    /// </summary>
    /// <returns>The selected filter and order strategy.</returns>
    public FilterOrderStrategy<T> SelectProjectStrategy()
    {
        var numOptions = ChoiceDict.Count;

        while (true)
        {
            DisplayMenu();

            var choice = NumberHandler.ReadInt(numOptions);

            if (choice == 0)
                return Strategy;

            ChoiceDict.TryGetValue(choice, out var action);
            action?.Invoke();
        }
    }

    /// <summary>
    /// Registers an action to be invoked when the user selects a certain option.
    /// </summary>
    /// <param name="key">The menu option to register the action for.</param>
    /// <param name="action">The action to perform when the menu option is selected.</param>
    protected void RegisterAction(int key, Action action)
        => ChoiceDict.Add(key, action);

    /// <summary>
    /// Clears the current filter strategy.
    /// </summary>
    protected void ClearFilter()
        => Strategy.FilterStrategy = null;

    /// <summary>
    /// Clears the current order strategy.
    /// </summary>
    protected void ClearOrder()
        => Strategy.OrderStrategy = null;
}