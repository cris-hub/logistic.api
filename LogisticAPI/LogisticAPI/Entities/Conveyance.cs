using LogisticAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LogisticAPI.Entities
{
    public class Conveyance
    {

        [Required]
        public string Id { get; set; }
        public TransportEnum TransportType { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}