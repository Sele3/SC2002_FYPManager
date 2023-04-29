using FYPManager.Controller.Utility;
using FYPManager.Entity;
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
}
