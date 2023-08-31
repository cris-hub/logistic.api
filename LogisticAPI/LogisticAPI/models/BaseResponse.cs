
using LogisticAPI.Enums;
using System.Text.Json.Serialization;

namespace LogisticAPI.Models
{
    public class BaseResponse
    {
        public string Action { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public List<ErrorTypeEnum> Errors { get; set; }

    }
}
