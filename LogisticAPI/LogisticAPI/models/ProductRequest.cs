using LogisticAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace LogisticAPI.models
{
    public class ProductRequest
    {
        public ProductRequest()
        {
        }

        public int Amount { get; set; }
        [Required]
        public DateTime DeliveryDay { get; set; }
        [Required]
        public string UserId { get; set; }
        public string ProductType { get; set; }
        public double Price { get; set; }
        public Conveyance Conveyance { get; set; }
        public Place Place { get; set; }
    }
}