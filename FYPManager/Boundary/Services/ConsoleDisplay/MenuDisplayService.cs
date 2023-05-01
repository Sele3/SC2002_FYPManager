using FYPManager.Boundary.Services.StrategySelector;
using FYPManager.Boundary.UserBoundary;

namespace FYPManager.Boundary.Services.ConsoleDisplay;

public interface IMenuDisplayable { }

public static class MenuDisplayService<T> where T : IMenuDisplayable
{
    private static readonly Dictionary<Type, string> classMap;

    static MenuDisplayService() => classMap = new Dictionary<Type, string>
        {
            { typeof(LoginBoundary), LOGIN_MENU },
            { typeof(StudentBoundary), STUDENT_MENU },
            { typeof(SupervisorBoundary), SUPERVISOR_MENU },
            { typeof(CoordinatorBoundary), COORDINATOR_MENU },
            { typeof(ProjectStrategySelector), PROJECT_STRATEGY_MENU}
        };

    public static string GetMenuDisplayText()
    {
        Console.Clear();
        return classMap[typeof(T)];
    }

    private const string LOGIN_MENU =
        $"┌────────────────────────────────────────┐\n" +
        $"│ <Enter 0 to shutdown system>           │\n" +
        $"│──────── Welcome to FYP Manager ────────│\n" +
        $"│ Login as                               │\n" +
        $"│ 1. Student                             │\n" +
        $"│ 2. Supervisor                          │\n" +
        $"│ 3. Coordinator                         │\n" +
        $"└────────────────────────────────────────┘\n" +
        $"Please select an option:";

    private const string STUDENT_MENU =
        $"╔═════════════════════════════════════╗\n" +
        $"║          Student FYP Menu           ║\n" +
        $"╟─────────────────────────────────────╢\n" +
        $"║    PROJECTS                         ║\n" +
        $"║ 1. View all available projects      ║\n" +
        $"║ 2. View my allocated project        ║\n" +
        $"║                                     ║\n" +
        $"║    REQUESTS                         ║\n" +
        $"║ 3. Request a project allocation     ║\n" +
        $"║ 4. Request a project title change   ║\n" +
        $"║ 5. Request a project deregistration ║\n" +
        $"║ 6. View my request history          ║\n" +
        $"║                                     ║\n" +
        $"║    SETTINGS                         ║\n" +
        $"║ 7. Change password                  ║\n" +
        $"╚═════════════════════════════════════╝\n" +
        $"Please select an option:";

    private const string SUPERVISOR_MENU =
        $"╔═════════════════════════════════════╗\n" +
        $"║         Supervisor FYP Menu         ║\n" +
        $"╟─────────────────────────────────────╢\n" +
        $"║    PROJECTS                         ║\n" +
        $"║ 1. Create a new project             ║\n" +
        $"║ 2. Update an existing project title ║\n" +
        $"║ 3. View my submitted projects       ║\n" +
        $"║                                     ║\n" +
        $"║    REQUESTS                         ║\n" +
        $"║ 4. View my pending requests         ║\n" +
        $"║ 5. View my request history          ║\n" +
        $"║ 6. Request a student transfer       ║\n" +
        $"║                                     ║\n" +
        $"║    SETTINGS                         ║\n" +
        $"║ 7. Change password                  ║\n" +
        $"╚═════════════════════════════════════╝\n" +
        $"Please select an option:";

    private const string COORDINATOR_MENU =
        $"╔═════════════════════════════════════╗\n" +
        $"║        Coordinator FYP Menu         ║\n" +
        $"╟─────────────────────────────────────╢\n" +
        $"║    PROJECTS                         ║\n" +
        $"║ 1. Create a new project             ║\n" +
        $"║ 2. Update an existing project title ║\n" +
        $"║ 3. View my submitted projects       ║\n" +
        $"║ 4. View all existing projects       ║\n" +
        $"║                                     ║\n" +
        $"║    REQUESTS                         ║\n" +
        $"║ 5. View all pending requests        ║\n" +
        $"║ 6. View all request history         ║\n" +
        $"║ 7. Request a student transfer       ║\n" +
        $"║                                     ║\n" +
        $"║    SETTINGS                         ║\n" +
        $"║ 8. Change password                  ║\n" +
        $"╚═════════════════════════════════════╝\n" +
        $"Please select an option:";

    private const string PROJECT_STRATEGY_MENU =
        $"┌──────────────────────────────┐\n" +
        $"│ Select filter and order:     │\n" +
        $"├────────────┬─────────────────┤\n" +
        $"│ Filter     │ Order           │\n" +
        $"├────────────┼─────────────────┤\n" +
        $"│ 1. None    │ 5. None         │\n" +
        $"│ 2. Title   │ 6. Title  (Asc) │\n" +
        $"│ 3. Status  │ 7. Title  (Dsc) │\n" +
        $"│ 4. Supvr.  │ 8. Supvr. (Asc) │\n" +
        $"│            │ 9. Supvr. (Dsc) │\n" +
        $"├────────────┴─────────────────┤\n" +
        $"│ 0. Back                      │\n" +
        $"└──────────────────────────────┘\n";
}

