using FYPManager.Controller.Utility.Strategy;
using FYPManager.Controller.Utility.Strategy.Projects;
using FYPManager.Entity.Projects;

namespace FYPManagerTests.StrategyTests;

/// <summary>
/// This class contains unit tests for the <see cref="FilterOrderStrategy{T}"/> class.
/// </summary>
[TestClass]
public class FilterOrderStrategyTest : BaseWithSeedDataTest
{
    private FilterOrderStrategy<Project> Strategy { get; } = new();
    private IEnumerable<Project> TestData { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [TestInitialize]
    public override void Setup()
    {
        base.Setup();
        TestData = _context.Projects.AsEnumerable();
    }

    /// <summary>
    /// Tests the default filter and order of the <see cref="FilterOrderStrategy{T}"/> class.
    /// </summary>
    [TestMethod]
    public void TestDefaultFilterAndOrder()
    {
        // Arrange
        var expectedProjects = _context.Projects.ToList();

        // Act
        var filteredAndOrderedProjects = Strategy
            .FilterAndOrder(TestData)
            .ToList();

        // Assert
        CollectionAssert.AreEqual(expectedProjects, filteredAndOrderedProjects);
    }

    /// <summary>
    /// Tests the filter by title functionality.
    /// </summary>
    [TestMethod]
    public void TestFilterByTitle()
    {
        // Arrange
        var expectedProjects = _context.Projects
            .Where(p => p.Title.Contains("Deep", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Act
        Strategy.FilterStrategy = new TitleFilterStrategy("Deep");
        var filteredProjects = Strategy
            .FilterAndOrder(TestData)
            .ToList();

        // Assert
        CollectionAssert.AreEqual(expectedProjects, filteredProjects);
    }

    /// <summary>
    /// Tests the order by supervisor name in descending order functionality.
    /// </summary>
    [TestMethod]
    public void TestOrderBySupervisorNameDesc()
    {
        // Arrange
        var expectedProjects = _context.Projects
            .OrderByDescending(p => p.Supervisor.Name)
            .ToList();

        // Act
        Strategy.OrderStrategy = new SupervisorOrderStrategy(isDescending: true);
        var orderedProjects = Strategy
            .FilterAndOrder(TestData)
            .ToList();

        // Assert
        CollectionAssert.AreEqual(expectedProjects, orderedProjects);
    }

    /// <summary>
    /// Tests the filter by supervisor name and order by title in ascending order functionality.
    /// </summary>
    [TestMethod]
    public void TestFilterBySupervisorNameAndOrderByTitleAsc()
    {
        // Arrange
        var expectedProjects = _context.Projects
            .Where(p => p.Supervisor.Name.Contains("AN", StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Title)
            .ToList();

        // Act
        Strategy.FilterStrategy = new SupervisorFilterStrategy("AN");
        Strategy.OrderStrategy = new TitleOrderStrategy(isDescending: false);
        var filteredAndOrderedProjects = Strategy
            .FilterAndOrder(TestData)
            .ToList();

        // Assert
        CollectionAssert.AreEqual(expectedProjects, filteredAndOrderedProjects);
    }
}
