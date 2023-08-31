using LogisticAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LogisticAPI.Entities
{
    public class Product
    {
        [Required]
        public string Id { get; set; }
        public int Amount { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime DeliveryDay { get; set; }
        [Required]
        public string UserId { get; set; }
        public string ConveyanceId { get; set; }
        public string PlaceId { get; set; }

        public string ProductType { get; set; }
        public double Price { get; set; }
        public DiscountEnum Discount { get; set; }
        public double FinalPrice { get; set; }

        public virtual Conveyance Conveyance { get; set; }
        public virtual Place Place { get; set; }
    }
}