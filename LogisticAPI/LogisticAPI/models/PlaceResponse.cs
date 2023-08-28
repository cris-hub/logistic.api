using LogisticAPI.Enums;

namespace LogisticAPI.models
{
    public class PlaceResponse
    {
        public PlaceEnum PlaceType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}