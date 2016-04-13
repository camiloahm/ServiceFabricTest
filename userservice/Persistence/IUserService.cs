using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using userservice.Model;

namespace userservice.Persistence
{
    interface IUserService
    {
        bool CreateUser(User user);
    }
}
