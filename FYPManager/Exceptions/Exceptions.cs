using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Exceptions;


public class LoginException : Exception
{
    public LoginException() : base("Invalid userID or password.") { }
    public LoginException(string message) : base(message) { }
}

public class AccountException : Exception
{
    public AccountException() : base("An error occured with the current account.") { }
    public AccountException(string message) : base(message) { }
}
