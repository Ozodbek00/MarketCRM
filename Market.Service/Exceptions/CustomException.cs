namespace Market.Service.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; }
        public string Message { get; }

        public CustomException(int code, string message)
            : base(message)
        {
            Code = code;
            Message = message;
        }
    }
}
