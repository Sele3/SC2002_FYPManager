using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Entity.Users;

public abstract class User
{
    [Key]
    public string UserID { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    public override string ToString() =>
        $"Name: {Name}\n" +
        $"Email: {Email}\n" +
        $"Password: {Password}";

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        
        var other = (User)obj;
        return UserID.Equals(other.UserID) &&
            Name.Equals(other.Name) &&
            Email.Equals(other.Email) &&
            Password.Equals(other.Password);
    }

    public override int GetHashCode()
        => HashCode.Combine(UserID, Name, Email, Password);
}
