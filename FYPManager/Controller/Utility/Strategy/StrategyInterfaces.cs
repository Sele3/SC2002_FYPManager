namespace FYPManager.Controller.Utility.Strategy;

/// <summary>
/// Represents a strategy used in the context of the Filter-Order pattern.
/// </summary>
public interface IStrategy
{
    /// <summary>
    /// Returns a string representation of the strategy.
    /// </summary>
    /// <returns>A string representation of the strategy.</returns>
    public string ToString();
}

/// <summary>
/// Represents a filter strategy used in the context of the Filter-Order pattern.
/// </summary>
/// <typeparam name="T">The type of elements being filtered.</typeparam>
public interface IFilterStrategy<T> : IStrategy
{
    /// <summary>
    /// Filters a sequence of elements.
    /// </summary>
    /// <param name="elements">The sequence of elements to filter.</param>
    /// <returns>A filtered sequence of elements.</returns>
    IEnumerable<T> Filter(IEnumerable<T> elements);
}

/// <summary>
/// Represents an order strategy used in the context of the Filter-Order pattern.
/// </summary>
/// <typeparam name="T">The type of elements being ordered.</typeparam>
public interface IOrderStrategy<T> : IStrategy
{
    /// <summary>
    /// Orders a sequence of elements.
    /// </summary>
    /// <param name="elements">The sequence of elements to order.</param>
    /// <returns>An ordered sequence of elements.</returns>
    IEnumerable<T> Order(IEnumerable<T> elements);
}

/// <summary>
/// Represents an object that can be used with a FilterOrderStrategy to filter and order a list of elements.
/// </summary>
/// <typeparam name="T">The type of elements being filtered and ordered.</typeparam>
public interface IStrategyCompatible<T>
{
    /// <summary>
    /// Returns a list of elements that satisfy the given <see cref="FilterOrderStrategy"/>.
    /// </summary>
    /// <param name="strategy">The <see cref="FilterOrderStrategy"/> to use for filtering and ordering the list of elements.</param>
    /// <returns>A list of elements that satisfy the given <see cref="FilterOrderStrategy"/>.</returns>
    public List<T> GetListUsingStrategy(FilterOrderStrategy<T> strategy);
}