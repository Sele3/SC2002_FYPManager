namespace FYPManager.Controller.Utility.Strategy;

public class FilterOrderStrategy<T>
{
    public IFilterStrategy<T>? FilterStrategy { private get; set; }
    public IOrderStrategy<T>? OrderStrategy { private get; set; }

    public IEnumerable<T> FilterAndOrder(IEnumerable<T> elements)
    {
        elements = FilterStrategy?.Filter(elements) ?? elements;
        elements = OrderStrategy?.Order(elements) ?? elements;
        return elements;
    }

    public override string ToString() =>
        $"Filter: {FilterStrategy?.ToString() ?? "None"}\n" +
        $"Order : {OrderStrategy?.ToString() ?? "None"} \n";
}