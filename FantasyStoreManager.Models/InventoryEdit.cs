using FantasyStoreManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Models
{
    public class InventoryEdit
    {
        public int InventoryId { get; set; }

        [Display(Name ="Product")]
        public int ProductId { get; set; }

        [Range(0, 1000000, ErrorMessage = "Quantity must be between 0 and 1000000")]
        public int Quantity { get; set; }

        public string Name { get; set; }

        public virtual Product Product { get; set; }
    }
}
