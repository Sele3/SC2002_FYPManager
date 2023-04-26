﻿using FYPManager.Entity.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Entity.Users;

public class Supervisor : User
{
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}