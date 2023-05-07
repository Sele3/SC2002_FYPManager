namespace FYPManager.Controller.Utility.Strategy;

/// <summary>
/// A class representing a strategy for filtering and ordering a collection of elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of elements to filter and order.</typeparam>
public class FilterOrderStrategy<T>
{
    /// <summary>
    /// The filter strategy to use for filtering the elements.
    /// </summary>
    public IFilterStrategy<T>? FilterStrategy { get; set; }
    /// <summary>
    /// The order strategy to use for ordering the elements.
    /// </summary>
    public IOrderStrategy<T>? OrderStrategy { get; set; }

    /// <summary>
    /// Filters and orders a collection of elements based on the filter and order strategies set in this object.
    /// </summary>
    /// <param name="elements">The collection of elements to filter and order.</param>
    /// <returns>The filtered and ordered collection of elements.</returns>
    public IEnumerable<T> FilterAndOrder(IEnumerable<T> elements)
    {
        elements = FilterStrategy?.Filter(elements) ?? elements;
        elements = OrderStrategy?.Order(elements) ?? elements;
        return elements;
    }

    /// <summary>
    /// Returns a string representation of this object, including the filter and order strategies set.
    /// </summary>
    /// <returns>A string representation of this object.</returns>
    public override string ToString() =>
        $"Filter: {FilterStrategy?.ToString() ?? "None"}\n" +
        $"Order : {OrderStrategy?.ToString() ?? "None"} \n";
}