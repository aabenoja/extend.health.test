using ExtendHealth.Web.Models.Home;
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
            return View(new IndexViewModel
            {
                TotalOnline = usersRepo.UsersOnline,
                Admins = usersRepo.Admins
            });
        }

        public ActionResult Contacts()
        {
            return View(new ContactsViewModel
            {
                Contacts = contactsRepo.GetAllNames
            });
        }
    }
}
