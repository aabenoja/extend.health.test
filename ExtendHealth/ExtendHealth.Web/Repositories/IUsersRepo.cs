using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Web.Repositories
{
    public interface IUsersRepo
    {
        int UsersOnline { get; }
        IEnumerable<string> Usernames { get; }
    }
}
