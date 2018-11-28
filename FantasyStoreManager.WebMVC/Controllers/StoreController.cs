﻿using FantasyStoreManager.Models;
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
            var model = new StoreListItem[0];
            return View(model);
        }
    }
}