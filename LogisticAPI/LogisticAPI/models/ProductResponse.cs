using LogisticAPI.Enums;
using LogisticAPI.Test.Repositories;
using System.ComponentModel.DataAnnotations;

namespace LogisticAPI.models
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime DeliveryDay { get; set; }
        [Required]
        public string UserId { get; set; }
        public string ProductType { get; set; }
        public double Price { get; set; }
        public Conveyance Conveyance { get; set; }
        public Place Place { get; set; }
        public DiscountEnum Discount { get; set; }
        public double FinalPrice { get; set; }
    }
}