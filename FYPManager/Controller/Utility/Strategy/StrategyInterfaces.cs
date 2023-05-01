namespace FYPManager.Controller.Utility.Strategy;

public interface IStrategy
{
    public string ToString();
}

public interface IFilterStrategy<T> : IStrategy
{
    IEnumerable<T> Filter(IEnumerable<T> elements);
}

public interface IOrderStrategy<T> : IStrategy
{
    IEnumerable<T> Order(IEnumerable<T> elements);
}

public interface IStrategyCompatible<T>
{
    public List<T> GetListUsingStrategy(FilterOrderStrategy<T> strategy);
}