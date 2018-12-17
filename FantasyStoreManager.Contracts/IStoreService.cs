using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Contracts
{
    public interface IStoreService
    {
        bool CreateStore(StoreCreate model);
        IEnumerable<StoreListItem> GetStores(string sortOrder);
        StoreDetail GetStoreById(int id);
        bool UpdateStore(StoreEdit model);
        bool DeleteStore(int id);
        List<Store> Stores();
    }
}
