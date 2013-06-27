using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendHealth.Web.Repositories.Concrete
{
    public class MockContactsRepo : IContactsRepo
    {
        public IEnumerable<string> GetAllNames
        {
            get
            {
                yield return "Jimmy";
                yield return "John";
                yield return "Michael";
            }
        }
    }
}