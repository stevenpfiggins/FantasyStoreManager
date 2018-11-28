using FantasyStoreManager.Data;
using FantasyStoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Services
{
    public class StoreService
    {
        private readonly Guid _userId;

        public StoreService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateStore(StoreCreate model)
        {
            var entity = new Store()
            {
                OwnerId = _userId,
                Name = model.Name,
                Location = model.Location,
                TypeofStore = model.TypeOfStore
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Stores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<StoreListItem> GetStores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Stores.Where(e => e.OwnerId == _userId).Select(e => new StoreListItem
                {
                    StoreId = e.StoreId,
                    Name = e.Name,
                    Location = e.Location,
                    TypeOfStore = e.TypeofStore
                });

                return query.ToArray();
            }
        }
    }

    
}
