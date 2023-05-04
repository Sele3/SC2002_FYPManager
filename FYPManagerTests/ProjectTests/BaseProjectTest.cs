using FYPManager.Controller.UserController;

namespace FYPManagerTests.ProjectTests;

/// <summary>
/// This class is the base class for all project tests.
/// </summary>
public abstract class BaseProjectTest : BaseWithSeedDataTest
{
    protected SupervisorController SupervisorController { get; set; } = null!;

    [TestInitialize]
    public override void Setup()
    {
        base.Setup();
        SupervisorController = new SupervisorController(_context);
    }
}
