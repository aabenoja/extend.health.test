using ExtendHealth.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendHealth.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUsersRepo repo;

        public HomeController()
        {
            //this.repo = repo;
        }

        public ActionResult Index()
        {
            return View(/*new
            {
                TotalOnline = repo.UsersOnline,
                UserList = repo.Usernames
            }*/);
        }

    }
}
