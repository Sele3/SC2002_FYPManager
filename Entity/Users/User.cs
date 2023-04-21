﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Entity.Users;

public abstract class User
{
    [Key]
    public string? UserID { get; set; }

    [Required]
    public string? Name { get; set; }
    
    [Required, EmailAddress]
    
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
