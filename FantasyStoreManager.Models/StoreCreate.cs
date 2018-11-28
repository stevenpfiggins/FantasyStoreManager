using FantasyStoreManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Models
{
    public class StoreCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(60, ErrorMessage = "The maximum length of this field is 60 characters.")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(35, ErrorMessage = "The maximum length of this field is 35 characters.")]
        public string Location { get; set; }
        [Required]
        public StoreType TypeOfStore { get; set; }
    }
}
