using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Contracts
{
    public interface IInventoryService
    {
        bool CreateInventory(int id, InventoryCreate model);
        IEnumerable<InventoryListItem> GetStoreInventories(int id, string sortOrder);
        IEnumerable<StoreWithUniqueProductListItem> GetStores(string sortOrder);
        ProductDetail GetProductById(int id);
        InventoryDetail GetInventoryById(int id);
        bool UpdateInventory(InventoryEdit model);
        bool DeleteInventory(int id);
        InventoryCreate CurrentInventory(int id);
        List<Store> Stores();
        List<Product> Products();
        List<Inventory> Inventories();
    }
}
