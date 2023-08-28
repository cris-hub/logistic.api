using LogisticAPI.Enums;

namespace LogisticAPI.models
{
    public class PlaceRequest
    {
        public PlaceEnum PlaceType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}