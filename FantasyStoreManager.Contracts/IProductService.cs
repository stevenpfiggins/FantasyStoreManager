using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Contracts
{
    public interface IProductService
    {
        bool CreateProduct(ProductCreate model);
        IEnumerable<ProductListItem> GetProducts(string sortOrder);
        ProductDetail GetProductById(int id);
        bool UpdateProduct(ProductEdit model);
        bool DeleteProduct(int id);
    }
}
