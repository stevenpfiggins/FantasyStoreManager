using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Data
{
    public class Inventory
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        [Display(Name = "Qty")]
        public int Quantity { get; set; }
    }
}
