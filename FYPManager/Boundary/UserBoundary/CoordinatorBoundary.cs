using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYPManager.Interfaces;

namespace FYPManager.Boundary.UserBoundary;

public class CoordinatorBoundary : BaseUserBoundary
{
    public override void Run()
    {
        Console.WriteLine("Coordinator Boundary");
    }
}
