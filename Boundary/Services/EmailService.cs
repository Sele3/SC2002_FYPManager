using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Boundary.Services;

internal static class EmailService
{
    private static bool IsValidEmail(string email)
        => email != null && new EmailAddressAttribute().IsValid(email);

    public static string GetUserID(string email)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException($"Invalid email address: {email}");

        return email.Split("@")[0];
    }
}
