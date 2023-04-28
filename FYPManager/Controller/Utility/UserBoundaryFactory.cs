using FYPManager.Boundary.UserBoundary;
using FYPManager.Entity.Users;
using Microsoft.Extensions.DependencyInjection;

namespace FYPManager.Controller.Utility;

public static class UserBoundaryFactory
{
    public static BaseUserBoundary GetUserBoundary<T>(IServiceProvider serviceProvider) where T : User
    {
        if (typeof(T) == typeof(Student))
            return serviceProvider.GetRequiredService<StudentBoundary>();

        else if (typeof(T) == typeof(Supervisor))
            return serviceProvider.GetRequiredService<SupervisorBoundary>();
        
        else if (typeof(T) == typeof(Coordinator))
            return serviceProvider.GetRequiredService<CoordinatorBoundary>();

        else
            throw new ArgumentException($"Invalid User Type: {typeof(T)}");   
    }
}