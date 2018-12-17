using FantasyStoreManager.Contracts;
using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Services
{
    public class InventoryService : IInventoryService
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

        public IEnumerable<InventoryListItem> GetStoreInventories(int storeId, string sortOrder)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Inventories
                    .Where(e => e.StoreId == storeId)
                    .Select(e =>
                    new InventoryListItem
                    {
                        InventoryId = e.InventoryID,
                        ProductId = e.Product.ProductId,
                        Name = e.Product.Name,
                        Description = e.Product.Description,
                        Quantity = e.Quantity
                    });
                var stores = from s in query
                             select s;
                switch (sortOrder)
                {
                    case "productName_desc":
                        stores = stores.OrderByDescending(s => s.Name);
                        break;
                    case "Quantity":
                        stores = stores.OrderBy(s => s.Quantity);
                        break;
                    case "quantity_desc":
                        stores = stores.OrderByDescending(s => s.Quantity);
                        break;
                    default:
                        stores = stores.OrderBy(s => s.Name);
                        break;
                }
                return stores.ToArray();
            }
        }

        public IEnumerable<StoreWithUniqueProductListItem> GetStores(string sortOrder)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Stores
                    .Where(e => e.OwnerId == _userId)
                    .Select(e =>
                    new StoreWithUniqueProductListItem
                    {
                        StoreId = e.StoreId,
                        Name = e.Name,
                        Location = e.Location,
                        TypeOfStore = e.TypeofStore,
                        UniqueProducts = ctx.Inventories.Where(i => i.StoreId == e.StoreId).ToList().Count()
                    });
                var stores = from s in query
                             select s;
                switch (sortOrder)
                {
                    case "storeName_desc":
                        stores = stores.OrderByDescending(s => s.Name);
                        break;
                    case "Location":
                        stores = stores.OrderBy(s => s.Location);
                        break;
                    case "location_desc":
                        stores = stores.OrderByDescending(s => s.Location);
                        break;
                    case "Count":
                        stores = stores.OrderBy(s => s.UniqueProducts);
                        break;
                    case "count_desc":
                        stores = stores.OrderByDescending(s => s.UniqueProducts);
                        break;
                    default:
                        stores = stores.OrderBy(s => s.Name);
                        break;
                }
                return stores.ToArray();
            }
        }

        public ProductDetail GetProductById(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Products
                    .Single(e => e.ProductId == productId);
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

        public InventoryDetail GetInventoryById(int inventoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Inventories
                    .Single(e => e.InventoryID == inventoryId);
                return new InventoryDetail
                {
                    InventoryId = entity.InventoryID,
                    ProductId = entity.ProductId,
                    Name = entity.Product.Name,
                    Quantity = entity.Quantity
                };
            }
        }

        public bool UpdateInventory(InventoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Inventories
                    .Single(e => e.InventoryID == model.InventoryId);
                entity.InventoryID = model.InventoryId;
                entity.ProductId = model.ProductId;
                entity.Quantity = model.Quantity;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInventory(int inventoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Inventories
                    .Single(e => e.InventoryID == inventoryId);
                ctx.Inventories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public InventoryCreate CurrentInventory(int storeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var model =
                    new InventoryCreate
                    {
                        Inventories = ctx
                        .Inventories
                        .Where(e => e.StoreId == storeId)
                        .Select(e =>
                        new InventoryListItem
                        {
                            InventoryId = e.InventoryID,
                            ProductId = e.ProductId,
                            Product = e.Product,
                            Quantity = e.Quantity,
                        }).ToList()
                    };
                return model;
            }
        }

        public List<Store> Stores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Stores.ToList();
            }
        }

        public List<Product> Products()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Products.ToList();
            }
        }

        public List<Inventory> Inventories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Inventories.ToList();
            }
        }
    }
}
