using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Exceptions;

public abstract class CustomException : Exception
{
    public CustomException(string message) : base(message) { }
}

public class LoginException : CustomException
{
    public LoginException() : base("Invalid userID or password.") { }
    public LoginException(string message) : base(message) { }
}

public class AccountException : CustomException
{
    public AccountException() : base("An error occured with the current account.") { }
    public AccountException(string message) : base(message) { }
}
