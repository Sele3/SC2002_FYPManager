namespace FYPManager.Exceptions;

/// <summary>
/// Base class for custom exceptions in FYP Manager
/// </summary>
public abstract class CustomException : Exception
{
    public CustomException(string message) : base(message) { }
}

/// <summary>
/// Exception thrown when there is an issue with login credentials
/// </summary>
public class LoginException : CustomException
{
    public LoginException() : base("Invalid userID or password. Please try again.") { }
    public LoginException(string message) : base(message) { }
}

/// <summary>
/// Exception thrown when there is an issue with an account
/// </summary>
public class AccountException : CustomException
{
    public AccountException() : base("An error occurred with the current account. Please try again.") { }
    public AccountException(string message) : base(message) { }
}