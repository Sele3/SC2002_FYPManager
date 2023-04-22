namespace FYPManager.Exceptions;

public class LoginException : Exception
{
    public LoginException() : base("Error. Invalid userID or password.") { }
    public LoginException(string message) : base(message) { }
}
