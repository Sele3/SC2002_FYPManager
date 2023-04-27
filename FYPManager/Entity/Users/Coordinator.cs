using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Entity.Users;

public class Coordinator : User
{
    public override string ToString() =>
        $"CoordinatorID: {UserID}\n"
        + base.ToString();
}
