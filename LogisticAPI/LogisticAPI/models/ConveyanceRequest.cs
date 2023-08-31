using LogisticAPI.Enums;

namespace LogisticAPI.Models
{
    public class ConveyanceRequest
    {
        public string Id { get; set; }
        public TransportEnum TransportType { get; set; }
    }
}