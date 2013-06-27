using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendHealth.Web.Models.Home
{
    public class IndexViewModel
    {
        public int TotalOnline { get; set; }
        public IEnumerable<string> Admins { get; set; }
    }
}