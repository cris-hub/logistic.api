namespace LogisticAPI.Models
{
    public class BaseResponse<R>
    {
        public R Bpdy { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public List<Error> Errors { get; set; }

    }

    public class Error
    {
        public string Type;
        public string Message;
    }
}
