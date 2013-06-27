using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendHealth.Web.Repositories.Concrete
{
    public class MockUsersRepo : IUsersRepo
    {
        private int _counter;

        public int UsersOnline
        {
            get
            {
                return ++_counter;
            }
        }

        public IEnumerable<string> Admins
        {
            get 
            {
                yield return "Devyn";
                yield return "Trinity";
                yield return "Sledge";
                yield return "Cooper";
            }
        }

        public MockUsersRepo()
        {
            _counter = 0;
        }
    }
}