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
    public class ProductService
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

        public IEnumerable<ProductListItem> GetProducts()
        {
            //List<ProductListItem> replaceproductListItems = new List<ProductListItem>();
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Products.Select(e => new ProductListItem
                {
                    ProductId = e.ProductId,
                    Name = e.Name,
                    Price = e.Price,
                    TypeOfProduct = e.TypeOfProduct.ToString(),
                    IsMagical = e.IsMagical
                });

                //foreach (var q in query)
                //{
                //    foreach (char c in q.TypeOfProduct.Skip<char>(1))
                //    {
                //        if (char.IsUpper(c))
                //        {
                //            var i = q.TypeOfProduct.IndexOf(c);
                //            q.TypeOfProduct.Insert(i, " ");
                //            //q.CharacterRace.Split(q.CharacterRace[c], " ");
                //        }
                //    }
                //    //if (q.CharacterRace.Split(  )
                //    //    q.CharacterRace = q.CharacterRace.Replace("_", " ");
                //    replaceproductListItems.Add(q);

                //};

                ////return replaceproductListItems.ToArray();
                return query.ToArray();
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

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
