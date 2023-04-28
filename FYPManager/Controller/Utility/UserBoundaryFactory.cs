using FYPManager.Boundary.UserBoundary;
using FYPManager.Entity.Users;
using Microsoft.Extensions.DependencyInjection;

namespace FYPManager.Controller.Utility;

/// <summary>
/// Utility class to get the correct user boundary for a given <see cref="User"/> type
/// </summary>
public static class UserBoundaryFactory
{
    /// <summary>
    /// Returns the correct <see cref="BaseUserBoundary"/> implementation for a given <see cref="User"/> type.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="User"/> object for which to get the corresponding boundary.</typeparam>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> object used to retrieve the required boundary.</param>
    /// <returns>The corresponding <see cref="BaseUserBoundary"/> implementation for the given <see cref="User"/> type.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid <see cref="User"/> type is provided.</exception>
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