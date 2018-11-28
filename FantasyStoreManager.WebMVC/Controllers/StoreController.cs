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
            var service = CreateStoreService();
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
            if (!ModelState.IsValid) return View(model);
            var service = CreateStoreService();
            if (service.CreateStore(model))
            {
                TempData["SaveResult"] = "Your store was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Store could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateStoreService();
            var model = svc.GetStoreById(id);
            return View(model);
        }

        private StoreService CreateStoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StoreService(userId);
            return service;
        }
    }
}