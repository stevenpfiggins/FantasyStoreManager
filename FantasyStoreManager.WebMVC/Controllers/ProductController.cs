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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var service = CreateProductService();
            var model = service.GetProducts();
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
        public ActionResult Create(ProductCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateProductService();
            if (service.CreateProduct(model))
            {
                TempData["SaveResult"] = "Your product was crafted.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Product could not be crafted.");
            return View(model);
        }

        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            return service;
        }
    }
}