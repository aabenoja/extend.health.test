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
        
        private IUsersRepo usersRepo;
        private IContactsRepo contactsRepo;

        public HomeController(IUsersRepo usersRepo, IContactsRepo contactsRepo)
        {
            this.usersRepo = usersRepo;
            this.contactsRepo = contactsRepo;
        }

        public ActionResult Index()
        {
            return View(new
            {
                TotalOnline = usersRepo.UsersOnline,
                UserList = usersRepo.Usernames
            });
        }

        public ActionResult Contacts()
        {
            return View(new
            {
                Contacts = contactsRepo.GetAllNames
            });
        }
    }
}
