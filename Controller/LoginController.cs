using FYPManager.Boundary.Services;
using FYPManager.Entity;
using FYPManager.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Controller;

public class LoginController
{
    private readonly FYPMContext _context;

    public LoginController(FYPMContext context)
    {
        _context = context;
    }

    public void LoginAs<T>(string username, string password) where T : User
    {
        //var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        //if (user != null)
        //{
        //    return true;
        //}
    }
}
