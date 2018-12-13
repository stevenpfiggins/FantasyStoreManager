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
        public ActionResult Index(string sortOrder)
        {
            var service = CreateInventoryService();
            var model = service.GetStores(sortOrder);
            ViewBag.StoreNameSort = String.IsNullOrEmpty(sortOrder) ? "storeName_desc" : "";
            ViewBag.LocationNameSort = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.CountSort = sortOrder == "Count" ? "count_desc" : "Count";

            string sortType = "";
            switch (sortOrder)
            {
                default:
                case "storeName_desc":
                    sortType = "name";
                    break;

                case "Location":
                case "location_desc":
                    sortType = "location";
                    break;

                case "Count":
                case "count_desc":
                    sortType = "count";
                    break;
            }
            ViewBag.CurrentSortOrder = sortType;

            return View(model);
        }

        //GET:
        public ActionResult Create(int id)
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
            var model = svc.CurrentInventory(id);
            return View(model);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, InventoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateInventoryService();
            var inventoryCheckList = service.Inventories();
            foreach (var inventory in inventoryCheckList)
            {
                if (inventory.StoreId == id && inventory.ProductId == model.ProductId)
                {
                    TempData["SaveResult"] = "Product is already in inventory. Please edit current inventory.";
                    return RedirectToAction("Edit", new { id = inventory.InventoryID });
                }
            }

            if (service.CreateInventory(id, model))
            {
                TempData["SaveResult"] = "Products were added to your inventory.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Product could not be added.");
            ViewBag.ProductId = new SelectList(service.Products(), "ProductId", "Name");

            return View(model);
        }

        public ActionResult InventoryIndex(int id, string sortOrder)
        {
            var svc = CreateInventoryService();
            var model = svc.GetStoreInventories(id, sortOrder);
            ViewBag.ProductNameSort = String.IsNullOrEmpty(sortOrder) ? "productName_desc" : "";
            ViewBag.QuantitySort = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";

            string sortType = "";
            switch (sortOrder)
            {
                default:
                case "productName_desc":
                    sortType = "name";
                    break;

                case "Quantity":
                case "quantity_desc":
                    sortType = "quantity";
                    break;
            }
            ViewBag.CurrentSortOrder = sortType;

            return View(model);
        }

        //GET:
        public ActionResult Edit(int id)
        {
            var service = CreateInventoryService();
            var detail = service.GetInventoryById(id);
            var model = new InventoryEdit
            {
                InventoryId = detail.InventoryId,
                ProductId = detail.ProductId,
                Name = detail.Name,
                Quantity = detail.Quantity
            };       
            return View(model);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.InventoryId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }
            var service = CreateInventoryService();
            if (service.UpdateInventory(model))
            {
                TempData["SaveResult"] = "Your inventory was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your inventory could not be updated.");
            return View();
        }

        //GET:
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateInventoryService();
            var model = svc.GetInventoryById(id);
            return View(model);
        }

        //DELETE:
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateInventoryService();
            service.DeleteInventory(id);
            TempData["SaveResult"] = "Your inventory has been removed.";
            return RedirectToAction("Index");
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