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
    public class InventoryController : ApiController
    {
        public IHttpActionResult GetAllStores()
        {
            var sortOrder = "";
            StoreService storeService = CreateStoreService();
            var stores = storeService.GetStores(sortOrder);
            return Ok(stores);
        }

        public IHttpActionResult Post(int id, InventoryCreate inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateInventoryService();

            if (!service.CreateInventory(id, inventory))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult GetAllInventory(int id)
        {
            var sortOrder = "";
            InventoryService inventoryService = CreateInventoryService();
            var inventories = inventoryService.GetStoreInventories(id, sortOrder);
            return Ok(inventories);
        }

        public IHttpActionResult Put(InventoryEdit inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateInventoryService();

            if (!service.UpdateInventory(inventory))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateInventoryService();

            if (!service.DeleteInventory(id))
                return InternalServerError();

            return Ok();
        }

        private InventoryService CreateInventoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var inventoryService = new InventoryService(userId);
            return inventoryService;
        }

        private StoreService CreateStoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var storeService = new StoreService(userId);
            return storeService;
        }
    }
}
