using FantasyStoreManager.Data;
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
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            var service = CreateInventoryService();
            var model = service.GetStores();

            return View(model);
        }

        //GET:
        public ActionResult Create()
        {
            var storeList = new SelectList(db.Stores, "StoreId", "Name");
            ViewBag.StoreId = storeList;
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateInventoryService();
            if (service.CreateInventory(model))
            {
                TempData["SaveResult"] = "Products were added to your inventory.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Product could not be added.");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "Name", model.StoreId);

            return View(model);
        }

        private InventoryService CreateInventoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new InventoryService(userId);
            return service;
        }

        private string PrivateEnumHelper(ProductType productType)
        {
            var item = EnumHelper<ProductType>.GetDisplayValue(productType);

            return item;
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}