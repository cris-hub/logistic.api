using LogisticAPI.Enums;

namespace LogisticAPI.Test.Repositories
{
    public class Place
    {
        public Place()
        {
        }

        public string Id { get; set; }
        public PlaceEnum PlaceType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}