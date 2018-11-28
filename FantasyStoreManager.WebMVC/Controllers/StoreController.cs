using FantasyStoreManager.Models;
using FantasyStoreManager.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasyStoreManager.WebMVC.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StoreService(userId);
            var model = service.GetStores();
            return View(model);
        }

        //GET:
        public ActionResult Create()
        {
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StoreService(userId);
            service.CreateStore(model);
            return RedirectToAction("Index");
        }
    }
}