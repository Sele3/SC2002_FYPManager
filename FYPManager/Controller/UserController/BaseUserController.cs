using FYPManager.Controller.Utility;
using FYPManager.Entity;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Controller.UserController;

public abstract class BaseUserController
{
    protected readonly FYPMContext _context;

    protected BaseUserController(FYPMContext context)
    {
        _context = context;
    }

    public void ChangePassword(string newPassword)
    {
        var currentPassword = UserSession.GetCurrentUser().Password;
        if (newPassword.Equals(currentPassword))
            throw new AccountException("New password cannot be the same as the current password.");
        
        UserSession.GetCurrentUser().Password = newPassword;
        _context.SaveChanges();
    }

    protected T GetUser<T>(string userID) where T : User
    {
        var users = _context.Set<T>();
        var user = users.Find(userID);
        return user ?? throw new AccountException("User not found.");
    }
}
