using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using FantasyStoreManager.Services;
using FantasyStoreManager.WebMVC.Models;
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
        public ActionResult Create(int id, CreateInventoryPassStoreId model)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = CreateInventoryService();
            var service = CreateStoreService();
            var storeDetail = service.GetStoreById(id);
            var viewModel = new CreateInventoryPassStoreId
            {
                StoreId = storeDetail.StoreId,
                Name = storeDetail.Name
            };
            ViewBag.StoreId = viewModel;
            var productList = new SelectList(svc.Products(), "ProductId", "Name");
            ViewBag.ProductId = productList;
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, InventoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateInventoryService();
            if (service.CreateInventory(id, model))
            {
                TempData["SaveResult"] = "Products were added to your inventory.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Product could not be added.");
            //ViewBag.StoreId = new SelectList(service.Stores(), "StoreId", "Name", model.StoreId);
            ViewBag.ProductId = new SelectList(service.Products(), "ProductId", "Name");

            return View(model);
        }

        public ActionResult InventoryIndex(int id)
        {
            var svc = CreateInventoryService();
            var model = svc.GetStoreInventories(id);

            return View(model);
        }

        public ActionResult ProductDetails(int id)
        {
            var svc = CreateInventoryService();
            var model = svc.GetProductById(id);
            model.TypeOfProductString = PrivateEnumHelper(model.TypeOfProduct);

            return RedirectToAction("Details", "Product");
        }

        private InventoryService CreateInventoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new InventoryService(userId);
            return service;
        }

        private StoreService CreateStoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StoreService(userId);
            return service;
        }

        private string PrivateEnumHelper(ProductType productType)
        {
            var item = EnumHelper<ProductType>.GetDisplayValue(productType);

            return item;
        }
    }
}