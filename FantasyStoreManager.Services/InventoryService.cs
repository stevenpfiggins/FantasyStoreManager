﻿using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Services
{
    public class InventoryService
    {
        private readonly Guid _userId;

        public InventoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateInventory(int id, InventoryCreate model)
        {
            var entity = new Inventory()
            {
                InventoryID = model.InventoryID,
                StoreId = id,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Inventories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InventoryListItem> GetStoreInventories(int storeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Inventories.Where( e => e.StoreId == storeId).Select(e => new InventoryListItem
                {
                    InventoryId = e.InventoryID,
                    ProductId = e.Product.ProductId,
                    Name = e.Product.Name,
                    Description = e.Product.Description,
                    Quantity = e.Quantity
                });

                return query.ToArray();
            }
        }

        public IEnumerable<StoreWithUniqueProductListItem> GetStores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Stores.Where(e => e.OwnerId == _userId).Select(e => new StoreWithUniqueProductListItem
                {
                    StoreId = e.StoreId,
                    Name = e.Name,
                    Location = e.Location,
                    TypeOfStore = e.TypeofStore,
                    UniqueProducts = ctx.Inventories.Where(i => i.OwnerId == _userId && i.StoreId == e.StoreId).ToList().Count()
                });

                return query.ToArray();
            }
        }

        public ProductDetail GetProductById(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Products.Single(e => e.ProductId == productId);
                return new ProductDetail
                {
                    ProductId = entity.ProductId,
                    Name = entity.Name,
                    Description = entity.Description,
                    TypeOfProduct = entity.TypeOfProduct,
                    Price = entity.Price,
                    IsMagical = entity.IsMagical
                };
            }
        }

        public IEnumerable<Store> Stores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Stores.ToList();
            }
        }

        public IEnumerable<Product> Products()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Products.ToList();
            }
        }
    }
}
