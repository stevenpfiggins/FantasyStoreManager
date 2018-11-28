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
        [Description("Ammunition")]
        Ammunition,
        [Description("Armor, Heavy")]
        HeavyArmor = 1,
        [Description("Armor, Light")]
        LightArmor,
        [Description("Armor, Medium")]
        MediumArmor,
        [Description("Good")]
        Good,
        [Description("Herb")]
        Herb,
        [Description("Kit")]
        Kit,
        [Description("Literature")]
        Literature,
        [Description("Mount")]
        Mount,
        [Description("Martial Melee")]
        MartialMelee,
        [Description("Martial Ranged")]
        MartialRanged,
        [Description("Pack")]
        Pack,
        [Description("Potion")]
        Potion,
        [Description("Shield")]
        Shield,
        [Description("Simple Melee")]
        SimpleMelee,
        [Description("Simple Ranged")]
        SimpleRanged,
        [Description("Tool")]
        Tool,
        [Description("Trinket")]
        Trinket,
        [Description("Vehicle")]
        Vehicle
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType TypeOfProduct { get; set; }
        public int Price { get; set; }
        public bool IsMagical { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
