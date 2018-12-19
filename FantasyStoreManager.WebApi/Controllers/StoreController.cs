using FantasyStoreManager.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FantasyStoreManager.WebApi.Controllers
{
    [Authorize]
    public class StoreController : ApiController
    {
        public IHttpActionResult Get()
        {
            var sortOrder = "";
            StoreService storeService = CreateStoreService();
            var stores = storeService.GetStores(sortOrder);
            return Ok(stores);
        }

        private StoreService CreateStoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var storeService = new StoreService(userId);
            return storeService;
        }
    }
}
