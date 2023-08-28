using LogisticAPI.Enums;

namespace LogisticAPI.Models
{
    public class ConveyanceResponse : BaseResponse<ConveyanceResponse>
    {
        public string Id { get; set; }
        public TransportEnum TransportType { get; set; }
    }
}