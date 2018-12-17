using FantasyStoreManager.Contracts;
using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Services
{
    public class ProductService : IProductService
    {
        
        private readonly Guid _userId;

        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateProduct(ProductCreate model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                TypeOfProduct = model.TypeOfProduct,
                Price = model.Price,
                IsMagical = model.IsMagical
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Products.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ProductListItem> GetProducts(string sortOrder)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Products.Select(e => new ProductListItem
                {
                    ProductId = e.ProductId,
                    Name = e.Name,
                    Price = e.Price,
                    TypeOfProduct = e.TypeOfProduct,
                    IsMagical = e.IsMagical
                });
                var products = from p in query
                             select p;
                switch (sortOrder)
                {
                    case "productName_desc":
                        products = products.OrderByDescending(p => p.Name);
                        break;
                    case "Price":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    case "ProductType":
                        products = products.OrderBy(p => p.TypeOfProduct);
                        break;
                    case "productType_desc":
                        products = products.OrderByDescending(p => p.TypeOfProduct);
                        break;
                    case "Magic":
                        products = products.OrderBy(p => p.IsMagical);
                        break;
                    case "non_magic":
                        products = products.OrderByDescending(p => p.IsMagical);
                        break;
                    default:
                        products = products.OrderBy(p => p.Name);
                        break;
                }
                return products.ToArray();
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

        public bool UpdateProduct(ProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Products.Single(e => e.ProductId == model.ProductId);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.TypeOfProduct = model.TypeOfProduct;
                entity.Price = model.Price;
                entity.IsMagical = model.IsMagical;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Products.Single(e => e.ProductId == productId);
                ctx.Products.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
