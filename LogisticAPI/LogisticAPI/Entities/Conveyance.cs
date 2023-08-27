using LogisticAPI.Enums;

namespace LogisticAPI.Test.Repositories
{
    public class Conveyance
    {
        public Conveyance()
        {
        }

        public string Id { get; set; }
        public TransportEnum TransportType { get; set; }
    }
}