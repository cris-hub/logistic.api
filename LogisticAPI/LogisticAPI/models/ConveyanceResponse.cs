using LogisticAPI.Enums;

namespace LogisticAPI.Models
{
    public class ConveyanceResponse : BaseResponse
    {
        public string Id { get; set; }
        public TransportEnum TransportType { get; set; }
        public string TransportTypeValue { get; set; } = string.Empty;
    }
}