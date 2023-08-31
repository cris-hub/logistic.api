using LogisticAPI.Enums;

namespace LogisticAPI.models
{
    public class PlaceResponse
    {
        public string Id { get; set; }
        public PlaceEnum PlaceType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}