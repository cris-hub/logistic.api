using LogisticAPI.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticAPI.Entities
{
    public class Place
    {
 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public PlaceEnum PlaceType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Product> Products { get; set; } = default!;
    }
}