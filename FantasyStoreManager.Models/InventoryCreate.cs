using FantasyStoreManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Models
{
    public class InventoryCreate
    {
        [Required]
        public int InventoryID { get; set; }

        [Display(Name ="Store Select")]
        public int StoreId { get; set; }

        [Display(Name ="Product Select")]
        public int ProductId { get; set; }

        [Range(0, 1000000, ErrorMessage = "Quantity must be between 0 and 1000000")]
        public int Quantity { get; set; }

        public List<InventoryListItem> Inventories { get; set; }

        public virtual Store Store { get; set; }
        public virtual Product Product { get; set; }
    }
}
