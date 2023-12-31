﻿using LogisticAPI.Entities;
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
        public string ProductType { get; set; }
        public double Price { get; set; }
        public string ConveyanceId { get; set; }
        public string PlaceId { get; set; }
    }
}