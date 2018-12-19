using FantasyStoreManager.Models;
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
        public IHttpActionResult GetAll()
        {
            var sortOrder = "";
            StoreService storeService = CreateStoreService();
            var stores = storeService.GetStores(sortOrder);
            return Ok(stores);
        }

        public IHttpActionResult Get(int id)
        {
            StoreService storeService = CreateStoreService();
            var store = storeService.GetStoreById(id);
            return Ok();
        }

        public IHttpActionResult Post(StoreCreate store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreService();

            if (!service.CreateStore(store))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(StoreEdit store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreService();

            if (!service.UpdateStore(store))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateStoreService();

            if (!service.DeleteStore(id))
                return InternalServerError();

            return Ok();
        }

        private StoreService CreateStoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var storeService = new StoreService(userId);
            return storeService;
        }
    }
}
