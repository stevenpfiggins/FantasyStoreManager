using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStoreManager.Data
{
    public enum ProductType
    {
        [Display(Name ="Ammunition")]
        Ammunition = 1,
        [Display(Name ="Armor, Heavy")]
        HeavyArmor,
        [Display(Name ="Armor, Light")]
        LightArmor,
        [Display(Name ="Armor, Medium")]
        MediumArmor,
        [Display(Name ="Attire")]
        Attire,
        [Display(Name ="Food/Drink")]
        FoodOrDrink,
        [Display(Name ="Good")]
        Good,
        [Display(Name ="Herb")]
        Herb,
        [Display(Name ="Kit")]
        Kit,
        [Display(Name ="Literature")]
        Literature,
        [Display(Name ="Mount")]
        Mount,
        [Display(Name ="Martial Melee")]
        MartialMelee,
        [Display(Name ="Martial Ranged")]
        MartialRanged,
        [Display(Name ="Pack")]
        Pack,
        [Display(Name ="Pet")]
        Pet,
        [Display(Name ="Potion")]
        Potion,
        [Display(Name ="Shield")]
        Shield,
        [Display(Name ="Simple Melee")]
        SimpleMelee,
        [Display(Name ="Simple Ranged")]
        SimpleRanged,
        [Display(Name ="Tool")]
        Tool,
        [Display(Name ="Trinket")]
        Trinket,
        [Display(Name ="Vehicle")]
        Vehicle,
        [Display(Name ="Other")]
        Other,
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product Select")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ProductType TypeOfProduct { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public bool IsMagical { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
