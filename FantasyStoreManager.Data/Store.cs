using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Data
{
    public enum StoreType
    {
        Apothecary = 1,
        General,
        Magic,
        Stablery,
        Smithy,
        Tannery,
        Tavern,
        Woodshop
    }
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public StoreType TypeofStore { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
